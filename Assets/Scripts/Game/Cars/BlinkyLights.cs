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

    [Header("Material swap (defaults to the parent's renderer)")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private int materialIndex;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;

    private bool canSwapMaterial;

    private void Awake()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        if (targetRenderer == null && transform.parent != null)
        {
            targetRenderer = transform.parent.GetComponent<Renderer>();
        }

        canSwapMaterial = SetUpMaterialSwap();
    }

    private bool SetUpMaterialSwap()
    {
        if (targetRenderer == null || offMaterial == null) return false;

        var materials = targetRenderer.sharedMaterials;
        if (materialIndex < 0 || materialIndex >= materials.Length)
        {
            Debug.LogWarning($"{name}: material index {materialIndex} is out of range for {targetRenderer.name}. Material swap disabled.", this);
            return false;
        }

        if (onMaterial == null)
        {
            onMaterial = materials[materialIndex];
        }

        return true;
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
        SetLight(startOn);

        while (true)
        {
            var wait = targetLight.enabled
                ? Random.Range(minOnTime, maxOnTime)
                : Random.Range(minOffTime, maxOffTime);

            yield return new WaitForSeconds(wait);
            SetLight(!targetLight.enabled);
        }
    }

    private void SetLight(bool on)
    {
        targetLight.enabled = on;

        if (!canSwapMaterial) return;

        var materials = targetRenderer.sharedMaterials;
        materials[materialIndex] = on ? onMaterial : offMaterial;
        targetRenderer.sharedMaterials = materials;
    }
}