using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [Space]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text wavesText;

    private void Update()
    {
        if (gameState.IsGameRunning)
        {
            healthSlider.value = gameState.Health;
            healthSlider.maxValue = gameState.MaxHealth;
            moneyText.text = $"{gameState.Money}";
            wavesText.text = $"{0}";
        }
        else
        {
            
        }
    }

    public void StartGame()
    {

    }

    public void StopGame()
    {

    }
}
