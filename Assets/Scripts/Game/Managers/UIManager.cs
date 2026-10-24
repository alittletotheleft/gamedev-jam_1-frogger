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

    private void Update()
    {
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
        }
        else
        {
            if (gameUI.activeSelf)
                gameUI.SetActive(false);
        }
    }
}
