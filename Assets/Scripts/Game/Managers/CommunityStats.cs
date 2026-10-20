using UnityEngine;

[CreateAssetMenu(fileName = "CommStats", menuName = "ScriptableObjects/Community Stats")]
public class CommunityStats : ScriptableObject
{
    // Percentage of max health healed after each wave
    private float healPercentage = 0f;
    // Amount of money made per second(?)
    private int moneyRate = 0;
    // Percentage of car's base speed re-added
    private float carSpeedPercentage = 0f;
    // Percentage of car's base damage re-added 
    private float carDamagePercentage = 0f;

    public float HealPercentage
    {
        get => healPercentage;
        set
        {
            healPercentage = Mathf.Clamp(value, 0f, Mathf.Infinity);
        }
    }
    public int MoneyRate
    {
        get => moneyRate;
        set
        {
            moneyRate = Mathf.Clamp(value, 0, 99999);
        }
    }
    public float CarSpeedPercentage
    {
        get => carSpeedPercentage;
        set
        {
            carSpeedPercentage = Mathf.Clamp(value, 0f, Mathf.Infinity);
        }
    }
    public float CarDamagePercentage
    {
        get => carDamagePercentage;
        set
        {
            carDamagePercentage = Mathf.Clamp(value, 0f, Mathf.Infinity);
        }
    }
}
