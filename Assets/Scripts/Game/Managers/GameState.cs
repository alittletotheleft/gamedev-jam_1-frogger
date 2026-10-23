using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/Game State")]
public class GameState : ScriptableObject
{
    private int health;
    private int maxHealth;
    private int money;
    private int waves;
    private int waveFrogCount;  
    
    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = Mathf.Clamp(value, 0, 99999);
        }
    }

    public int Money
    {
        get => money;
        set
        {
            money = Mathf.Clamp(value, 0, 99999);
        }
    }

    public int Waves
    {
        get => waves;
        set
        {
            waves = Mathf.Clamp(value, 0, 99999);
        }
    }

    public int WaveFrogCount
    {
        get => waveFrogCount;
        set
        {
            waveFrogCount = Mathf.Clamp(value, 0, 99999);
        }
    }

    public bool IsGameRunning { get; set; }
}
