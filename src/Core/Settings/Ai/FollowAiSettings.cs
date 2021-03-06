using System.Collections.Generic;

using Newtonsoft.Json;

namespace MissionControl.Config {
  public class FollowAiSettings {
    [JsonProperty("Target")]
    public string Target { get; set; } = "HeaviestMech";    // HeaviestMech, FirstLanceMember

    [JsonProperty("StopWhen")]
    public string StopWhen { get; set; } = "OnEnemyDetected";    // OnEnemyDetected, OnEnemyVisible, WhenNotNeeded

    [JsonProperty("MaxDistanceFromTargetBeforeSprinting")]
    public float MaxDistanceFromTargetBeforeSprinting { get; set; } = 200;
  }
}