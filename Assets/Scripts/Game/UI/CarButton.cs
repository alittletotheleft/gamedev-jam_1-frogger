using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CarButton : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private Car car;
    [SerializeField] private CarSelectEvent OnCarSelect;
    [Space]
    [SerializeField] private int waveUnlock = 0;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text cooldownText;
    private Button button;
    private bool isCooldown;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectCar);

        titleText.SetText($"{car.GetName()}");
    }

    private void Update()
    {
        moneyText.SetText($"$: {car.GetCost()}");
        cooldownText.SetText($"C: {car.GetCooldown():F1}");
        if (!isCooldown)
        {
            if (gameState.Waves < waveUnlock)
            {
                if (button.interactable)
                    button.interactable = false;
            }
            else
            {
                if (!button.interactable)
                    button.interactable = true;
            }
        }
    }

    private void SelectCar()
    {
        OnCarSelect.Invoke(car.gameObject, OnSelectComplete);
    }

    private void OnSelectComplete()
    {
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        button.interactable = false;
        float time = car.GetCooldown();
        isCooldown = true;

        while (time > 0)
        {
            cooldownText.SetText($"C: {time:F1}");
            time -= Time.deltaTime;
            yield return null;
        }

        button.interactable = true;
        cooldownText.SetText($"C: {car.GetCooldown():F1}");
        isCooldown = false;
    }

    private void OnDrawGizmos()
    {
        titleText.SetText($"{car.GetName()}");
        moneyText.SetText($"$: {car.GetCost()}");
        cooldownText.SetText($"C: {car.GetCooldown():F1}");
    }
}