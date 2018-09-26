using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using HBS.Logging;
using Harmony;
using Newtonsoft.Json;
using System.Reflection;

using MissionControl.Config;
using MissionControl.Utils;

namespace MissionControl {
    public class Main {
        public static ILog Logger;
        public static Config.Settings Settings { get; private set; }
        public static Assembly MissionControlAssembly { get; set; } 
        public static AssetBundle MissionControlBundle { get; set; }
        public static string Path { get; private set; }

        public static void InitLogger(string modDirectory) {
            Dictionary<string, LogLevel> logLevels = new Dictionary<string, LogLevel> {
                ["MissionControl"] = LogLevel.Debug
            };
            LogManager.Setup(modDirectory + "/output.log", logLevels);
            Logger = LogManager.GetLogger("MissionControl");
            Path = modDirectory;
        }

        // Entry point into the mod, specified in the `mod.json`
        public static void Init(string modDirectory, string modSettings) {
            try {
                InitLogger(modDirectory);
                LoadSettings(modDirectory);
            } catch (Exception e) {
                Logger.LogError(e);
                Logger.Log("Error loading mod settings - using defaults.");
                Settings = new Config.Settings();
            }

            HarmonyInstance harmony = HarmonyInstance.Create("co.uk.cwolf.MissionControl");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        private static void LoadSettings(string modDirectory) {
            Logger.Log("Loading MissionControl settings");
            string settingsJsonString = File.ReadAllText($"{modDirectory}/settings.json");
            Settings = JsonConvert.DeserializeObject<Config.Settings>(settingsJsonString);
        }
    }
}