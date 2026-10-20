using UnityEngine;

[RequireComponent(typeof(FrogsManager))]
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
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
        gameState.MaxHealth = 100;
        gameState.Health = 100;
        gameState.Money = 100;
        
        frogsManager.StartGame();
        uIManager.StartGame();
        gameState.IsGameRunning = true;
    }

    private void StopGame()
    {
        frogsManager.StopGame();
        gameState.IsGameRunning = false;
    }
}
