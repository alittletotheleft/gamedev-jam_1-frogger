using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CarButton : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private CarSelectEvent OnCarSelect;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private float cooldown = 6f;
    private Button button;
    private bool isOnCooldown;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectCar);
        isOnCooldown = false;
        cooldownText.text = $"C: {cooldown:F1}";
    }
    
    private void SelectCar()
    {
        OnCarSelect.Invoke(car, OnSelectComplete);
    }

    private void OnSelectComplete()
    {
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        button.interactable = false;
        float time = cooldown;

        while (time > 0)
        {
            cooldownText.text = $"C: {time:F1}";
            time -= Time.deltaTime;
            yield return null;
        }
        
        isOnCooldown = false;
        button.interactable = true;
        cooldownText.text = $"C: {cooldown:F1}";
    }
}
