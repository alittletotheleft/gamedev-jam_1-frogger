using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/Game State")]
public class GameState : ScriptableObject
{
    public int Health
    {
        get => Health;
        set
        {
            Health = Mathf.Clamp(value, 0, MaxHealth);
        }
    }

    public int MaxHealth
    {
        get => MaxHealth;
        set
        {
            MaxHealth = Mathf.Clamp(value, 0, 99999);
        }
    }

    public int Money
    {
        get => Money;
        set
        {
            Money = Mathf.Clamp(value, 0, 99999);
        }
    }

    public bool IsGameRunning { get; set; }
}
