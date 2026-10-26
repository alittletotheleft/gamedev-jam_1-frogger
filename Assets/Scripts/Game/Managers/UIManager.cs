using System.Collections;
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

    [Header("Wave Announcement Flash")]
    [SerializeField] private TMP_Text waveBannerText;
    [SerializeField] private float fadeInDuration = 0.25f;
    [SerializeField] private float displayDuration = 1.0f;
    [SerializeField] private float fadeOutDuration = 0.4f;

    private Coroutine waveBannerRoutine;

    private void Start()
    {
        if (waveBannerText != null)
        {
            waveBannerText.gameObject.SetActive(false);
        }
    }

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
    
    public void ShowWaveBanner(int waveNumber)
    {
        if (waveBannerText == null) return;

        if (waveBannerRoutine != null)
            StopCoroutine(waveBannerRoutine);

        waveBannerRoutine = StartCoroutine(FlashWaveBannerRoutine(waveNumber));
    }

    private IEnumerator FlashWaveBannerRoutine(int waveNumber)
    {
        waveBannerText.SetText("WAVE {0}", waveNumber);
        waveBannerText.gameObject.SetActive(true);

        // 1. Fade in
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            waveBannerText.alpha = Mathf.Clamp01(elapsed / fadeInDuration);
            yield return null;
        }
        waveBannerText.alpha = 1f;

        // 2. Hold on screen
        yield return new WaitForSeconds(displayDuration);

        // 3. Fade out
        elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            waveBannerText.alpha = Mathf.Clamp01(1f - (elapsed / fadeOutDuration));
            yield return null;
        }

        waveBannerText.alpha = 0f;
        waveBannerText.gameObject.SetActive(false);
        waveBannerRoutine = null;
    }
}