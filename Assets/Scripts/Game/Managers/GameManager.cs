using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public enum State {
    Wave, Upgrades
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int startingMoney = 300;
    [SerializeField] private int passiveMoney = 25;
    [SerializeField] private float moneyFrequency = 2f;
    [SerializeField] private int upgradeFrequency = 5;
    [SerializeField] private CommunityStat heal;
    // [SerializeField] private CommunityStat maxHealth;
    [SerializeField] private CommunityStat money;
    private FrogsManager frogsManager;
    private UIManager uIManager;
    private UpgradesManager upgradesManager;
    private bool isGeneratingMoney;

    private void Start()
    {
        frogsManager = GetComponentInChildren<FrogsManager>();
        uIManager = GetComponentInChildren<UIManager>();
        upgradesManager = GetComponentInChildren<UpgradesManager>();

        gameState.IsGameRunning = false;
        isGeneratingMoney = false;

        StartGame();
    }

    private void StartGame()
    {
        upgradesManager.InitializeStats();

        gameState.MaxHealth = startingHealth;
        gameState.Health = startingHealth;
        gameState.Money = startingMoney;
        gameState.Waves = 0;
        gameState.FrogsKilled = 0;

        gameState.IsGameOver = false;
        gameState.IsGameRunning = true;
        StartNextWave();
    }

    private void StopGame()
    {
        frogsManager.StopFrogSpawning();
        gameState.IsGameRunning = false;
        gameState.IsGameOver = true;
        Time.timeScale = 0f;
    }

    private void StartNextWave()
    {
        gameState.Waves++;
        gameState.State = State.Wave;
        frogsManager.StartFrogSpawning();
        if (!isGeneratingMoney)
        {
            isGeneratingMoney = true;
            StartCoroutine(GenerateMoney());
        }
    }

    private void StartUpgrades()
    {
        gameState.State = State.Upgrades;
        upgradesManager.InitializeUpgrades();
        isGeneratingMoney = false;
    }

    private IEnumerator GenerateMoney()
    {
        while (isGeneratingMoney)
        {
            yield return new WaitForSeconds(moneyFrequency);
            gameState.Money += Mathf.FloorToInt(passiveMoney + 0.50f * ((float)money.Level/money.MaxLevel));
        }
    }

    public void OnFrogEnter(int damage)
    {
        if (!gameState.IsGameRunning || gameState.State == State.Upgrades)
            return;

        gameState.Health -= damage;
        if (gameState.Health <= 0)
        {
            StopGame();
        }
    }

    public void OnFrogDestroy()
    {
        if (gameState.State == State.Upgrades)
            return;
        
        // Debug.Log("Frog destroyed :(");
        gameState.WaveCount--;

        if (gameState.WaveCount <= 0 && gameState.IsGameRunning)
        {
            gameState.Health += Mathf.FloorToInt(gameState.MaxHealth * (0.60f * ((float)heal.Level/heal.MaxLevel)));
            // gameState.Money += Mathf.FloorToInt(gameState.Money + 0.50f * ((float)money.Level+1/money.MaxLevel+1));
            StartNextWave();
            if (gameState.Waves % upgradeFrequency == 0)
            {
                StartUpgrades();
            }
        }
    }

    public void OnUpgradeSelect()
    {
        gameState.FrogLevel++;
        StartNextWave();
    }
}
