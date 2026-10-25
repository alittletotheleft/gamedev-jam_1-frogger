using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        gameObject.SetActive(false);
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = currentValue;
        gameObject.SetActive(currentValue < maxValue);
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}