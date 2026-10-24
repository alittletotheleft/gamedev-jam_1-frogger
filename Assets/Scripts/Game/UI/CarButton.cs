using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CarButton : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private CarSelectEvent OnCarSelect;
    
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text cooldownText;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectCar);

        titleText.text = $"{car.GetName()}";
        moneyText.text = $"$: {car.GetCost()}";
        cooldownText.text = $"C: {car.GetCooldown():F1}";
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

        while (time > 0)
        {
            cooldownText.text = $"C: {time:F1}";
            time -= Time.deltaTime;
            yield return null;
        }

        button.interactable = true;
        cooldownText.text = $"C: {car.GetCooldown():F1}";
    }
}