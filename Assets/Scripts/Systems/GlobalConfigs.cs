using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GlobalConfigs", fileName = "globalConfig")]
public class GlobalConfigs : ScriptableObject
{
    public float toyTimeAfterJoinBeforeScore = 2f;
    public float autoCreateMissionDelay = 0.5f; // when no missions, time to create next
}
