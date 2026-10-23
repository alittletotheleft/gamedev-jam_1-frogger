using UnityEngine;
using UnityEngine.Serialization;

public enum ModelSize
{
    Small = 0,
    Medium = 1,
    Large = 2
}

public enum ModelSpeed
{
    VerySlow = 0,
    Slow = 1,
    Normal = 2,
    Fast = 3
}

[CreateAssetMenu(fileName = "CarModel", menuName = "ScriptableObjects/Cars/CarModel")]
public class CarModel : ScriptableObject
{
    [Header("Basic Information")]
    public string modelName = "Lightning Mcqueen";
    public ModelSize size = ModelSize.Medium;
    public ModelSpeed speedType =  ModelSpeed.Normal;
    
    [Header("Gameplay")]
    public int cost = 100;
    public float cooldown = 6f;
    public string pathTrajectory = "coming soon";

    private readonly float[,] damageMatrix =
    {
        {0, 0, 50},
        {0, 0, 70},
        {0, 60, 100},
        {55, 80, 120}
    };

    private readonly float[] carSpeeds = {23f, 34.375f, 50f, 78.125f};

    public float getBaseSpeed()
    {
        return carSpeeds[(int)speedType];
    }

    public float getBaseDamage()
    {
        return damageMatrix[(int)speedType, (int)size];
    }
}
