using System.Collections;
using UnityEngine;

public class BlinkyLights : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    [SerializeField] private bool startOn = true;

    [Header("Seconds the light stays on")]
    [SerializeField] private float minOnTime = 0.5f;
    [SerializeField] private float maxOnTime = 2f;

    [Header("Seconds the light stays off")]
    [SerializeField] private float minOffTime = 0.5f;
    [SerializeField] private float maxOffTime = 3f;

    private void Awake()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }
    }

    private void OnEnable()
    {
        if (targetLight == null)
        {
            Debug.LogError($"{name}: no Light assigned or found on this object.", this);
            enabled = false;
            return;
        }

        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        targetLight.enabled = startOn;

        while (true)
        {
            var wait = targetLight.enabled
                ? Random.Range(minOnTime, maxOnTime)
                : Random.Range(minOffTime, maxOffTime);

            yield return new WaitForSeconds(wait);
            targetLight.enabled = !targetLight.enabled;
        }
    }
}