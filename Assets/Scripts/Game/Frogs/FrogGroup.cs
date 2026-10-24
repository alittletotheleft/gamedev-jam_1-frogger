using System;
using UnityEngine;

// [CreateAssetMenu(fileName = "FrogGroup", menuName = "ScriptableObjects/Frogs/Frog Group")]
// public class FrogGroup : ScriptableObject
[Serializable]
public class FrogGroup
{
    public GameObject frog;
    [Tooltip("Minimum number of frogs to spawn.")]
    public int minimumSpawnCount = 5;
    [Tooltip("Maximum number of frogs to spawn. Used only if 'isRandomCount' is turned on.")]
    public int maximumSpawnCount = 10;
    [Tooltip("Determines if the number of frogs to spawn in the wave is random between the minimum and maximum spawn counts.")]
    public bool isRandomCount = false;
    [Tooltip("Minimum time between frog spawns.")]
    public float minimumSpawnCooldown = 0.75f;
    [Tooltip("Maximum time between frog spawns. Used only if 'isRandomCooldown' is turned on.")]
    public float maximumSpawnCooldown = 1.5f;
    [Tooltip("Determines if the number of frogs to spawn in the wave is random between the minimum and maximum spawn counts.")]
    public bool isRandomCooldown = false;
}