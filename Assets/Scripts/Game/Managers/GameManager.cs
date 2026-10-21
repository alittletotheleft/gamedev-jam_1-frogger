using UnityEngine;

[RequireComponent(typeof(FrogsManager))]
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int startingMoney = 300;
    private FrogsManager frogsManager;
    private UIManager uIManager;

    private void Start()
    {
        frogsManager = GetComponent<FrogsManager>();
        uIManager = GetComponent<UIManager>();

        gameState.IsGameRunning = false;

        StartGame();
    }

    private void StartGame()
    {
        gameState.MaxHealth = startingHealth;
        gameState.Health = startingHealth;
        gameState.Money = startingMoney;
        gameState.Waves = 1;
        
        frogsManager.StartGame();
        uIManager.StartGame();
        gameState.IsGameRunning = true;
    }

    private void StopGame()
    {
        frogsManager.StopGame();
        gameState.IsGameRunning = false;
    }

    public void OnFrogEnter(int damage)
    {
        gameState.Health -= damage;
        Debug.Log($"Health: {gameState.Health}");
        if (gameState.Health <= 0)
        {
            StopGame();
        }
    } 
}
