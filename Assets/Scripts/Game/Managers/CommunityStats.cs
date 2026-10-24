using System;
using UnityEngine;

[Serializable]
public struct CommunityStat
{
    private int level;
    private int maxLevel;

    public CommunityStat(int _level, int _maxLevel)
    {
        level = _level;
        maxLevel = _maxLevel;
    }

    public int Level
    {
        get => level;
        set
        {
            level = Mathf.Clamp(value, 1, maxLevel);
        }
    }

    public int MaxLevel
    {
        get => level;
        set
        {
            maxLevel = Mathf.Clamp(value, 1, 99999);
        }
    }
}

[CreateAssetMenu(fileName = "CommStats", menuName = "ScriptableObjects/Community Stats")]
public class CommunityStats : ScriptableObject
{
    // Percentage of max health healed after each wave
    private CommunityStat healLevel = new(1, 7);
    // Amount of money made per second(?)
    private CommunityStat moneyLevel = new(1, 7);
    // Percentage of car's base speed re-added
    private CommunityStat carSpeedLevel = new(1, 7);
    // Percentage of car's base damage re-added 
    private CommunityStat carDamageLevel = new(1, 7);

    public CommunityStat HealLevel { get => healLevel; set { healLevel = value; } }
    public CommunityStat MoneyLevel { get; set; }
    public CommunityStat CarSpeedLevel { get; set; }
    public CommunityStat CarDamageLevel { get; set; }
}
