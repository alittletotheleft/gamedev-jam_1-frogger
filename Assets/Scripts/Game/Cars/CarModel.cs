using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

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

[System.Serializable]
public struct PointSpeed
{
    public int pointIndexApproaching;
    public ModelSpeed speed;
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
    
    [Header("Pathing")]
    public LineRenderer pathTrajectory;
    public Vector3 pathOffset = new Vector3(0f, 0f, -8.5f);
    [Tooltip("Index is the point the car is approaching. The car starts at 0 approaching 1")]
    public List<PointSpeed> pointSpeeds = new List<PointSpeed>();

    private readonly int[,] damageMatrix =
    {
        {0, 0, 70},
        {0, 50, 85},
        {0, 60, 100},
        {30, 90, 120}
    };

    private readonly float[] carSpeeds = {23f, 34.375f, 50f, 78.125f};
    
    private readonly float[] rotationSpeeds = {5f, 7f, 8f, 10f};

    public float GetBaseSpeed()
    {
        return carSpeeds[(int)speedType];
    }

    public float GetSpeed(ModelSpeed speed)
    {
        return carSpeeds[(int)speed];
    }

    public int GetBaseDamage()
    {
        return damageMatrix[(int)speedType, (int)size];
    }

    public int GetDamage(ModelSpeed speed)
    {
        return damageMatrix[(int)speed, (int)size];
    }
    
    public float GetRotationSpeed()
    {
        return rotationSpeeds[(int)speedType];
    }
    
}
