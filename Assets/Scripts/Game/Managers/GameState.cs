using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/Game State")]
public class GameState : ScriptableObject
{
    private State state;
    private int health;
    private int maxHealth;
    private int money;
    private int waves;
    private int waveCount;
    
    public bool IsGameOver { get; set; }
    public int FrogsKilled { get; set; }
    
    public State State
    {
        get => state;
        set
        {
            state = value;
        }
    }

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

    public int WaveCount
    {
        get => waveCount;
        set
        {
            waveCount = Mathf.Clamp(value, 0, 99999);
        }
    }

    public bool IsGameRunning { get; set; }
}
