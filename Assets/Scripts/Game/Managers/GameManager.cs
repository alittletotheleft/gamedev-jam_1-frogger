using UnityEngine;

[RequireComponent(typeof(FrogsManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    private FrogsManager frogsManager;

    private void Start()
    {
        frogsManager = GetComponent<FrogsManager>();

        gameState.IsGameRunning = false;

        StartGame();
    }

    private void StartGame()
    {
        gameState.MaxHealth = 100;
        gameState.Health = 100;
        gameState.Money = 100;
        
        frogsManager.StartGame();
        gameState.IsGameRunning = true;
    }

    private void StopGame()
    {
        frogsManager.StopGame();
        gameState.IsGameRunning = false;
    }
}
