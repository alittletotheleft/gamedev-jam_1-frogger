using UnityEngine;

public enum FrogSize
{
    Small = 0,
    Medium = 1,
    Large = 2,
    Giga = 3
}

[CreateAssetMenu(fileName = "FrogType", menuName = "ScriptableObjects/Frogs/FrogType")]
public class FrogType : ScriptableObject
{
    public FrogSize size;
    public int baseHealth;
    public float baseSpeed;
    public float minHopDelay;
    public float maxHopDelay;
    public int baseDamage;
    public int baseRewards;
    
    private readonly float healthMultiplier = 0.15f;
    private readonly float speedMultiplier = 0.2f;
    private readonly float damageMultiplier = 0.15f;

    public int GetComputedHealth(int level)
    {
        return Mathf.RoundToInt(baseHealth * (1 + level * healthMultiplier));
    }

    public int GetComputedDamage(int level)
    {
        return  Mathf.RoundToInt(baseDamage * (1 + level * damageMultiplier));
    }

    public float GetComputedSpeed(int level)
    {
        return (baseSpeed + level * speedMultiplier);
    }
}
