using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [Space]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject upgradesUI;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text wavesText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TMP_Text wavesDefeatedText;
    [SerializeField] private TMP_Text frogsKilledText;

    private void Update()
    {
        if (gameOverUI.activeSelf != gameState.IsGameOver)
        {
            gameOverUI.SetActive(gameState.IsGameOver);
            if (gameState.IsGameOver)
            {
                wavesDefeatedText.SetText("Waves Defeated: {0}", Mathf.Max(0, gameState.Waves - 1));
                frogsKilledText.SetText("Frogs Squashed: {0}", gameState.FrogsKilled);
            }
        }
        
        if (gameState.IsGameRunning)
        {
            if (!gameUI.activeSelf)
                gameUI.SetActive(true);
            healthSlider.value = gameState.Health;
            healthSlider.maxValue = gameState.MaxHealth;
            // moneyText.text = $"{gameState.Money}";
            // wavesText.text = $"{gameState.Waves}";
            moneyText.SetText("{0}", gameState.Money);
            wavesText.SetText("{0}", gameState.Waves);
            
            if (gameState.State == State.Upgrades)
            {
                if (!upgradesUI.activeSelf)
                    upgradesUI.SetActive(true);
            }
            else
            {
                if (upgradesUI.activeSelf)
                    upgradesUI.SetActive(false);
            }
        }
        else
        {
            if (gameUI.activeSelf)
                gameUI.SetActive(false);
        }
    }
}
