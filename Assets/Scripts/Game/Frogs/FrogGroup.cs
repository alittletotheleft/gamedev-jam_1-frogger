using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class FrogGroup
{
    [Header("Prefabs")]
    public GameObject smallFrog;
    public GameObject mediumFrog;
    public GameObject largeFrog;
    public GameObject gigaFrog;

    [Header("Counts")]
    [Tooltip("Total group size")]
    public int minimumTotal = 1;
    public int maximumTotal = 10;
    [Tooltip("Maximum large frogs. Minimum is 0.")]
    public int maximumLarge = 4;
    [Tooltip("If true, one giga frog is added to this group.")]
    public bool spawnGiga = false;
    
    [Header("Cooldown")]
    [Tooltip("Seconds after the wave starts before this group's first frog spawns.")]
    public float startDelay = 0f;
    public float minimumSpawnCooldown = 0.75f;
    public float maximumSpawnCooldown = 1.5f;
    
    public List<GameObject> BuildSpawnList()
    {
        bool hasSmall = smallFrog != null;
        bool hasMedium = mediumFrog != null;
        bool hasLarge = largeFrog != null;
        bool hasGiga = gigaFrog != null;

        int total = Random.Range(minimumTotal, maximumTotal + 1);

        int large = hasLarge ? Random.Range(0, Mathf.Min(maximumLarge, total) + 1) : 0;
        int rest = total - large;

        int small = 0;
        int medium = 0;
        if (hasSmall && hasMedium)
        {
            small = Random.Range(0, rest + 1);
            medium = rest - small;
        }
        else if (hasSmall)
        {
            small = rest;
        }
        else if (hasMedium)
        {
            medium = rest;
        }

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < small; i++) list.Add(smallFrog);
        for (int i = 0; i < medium; i++) list.Add(mediumFrog);
        for (int i = 0; i < large; i++) list.Add(largeFrog);
        if (spawnGiga && hasGiga) list.Add(gigaFrog);

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        return list;
    }
}