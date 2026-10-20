using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/Game State")]
public class GameState : ScriptableObject
{
    private int health;
    private int maxHealth;
    private int money;
    
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

    public bool IsGameRunning { get; set; }
}
