using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public float minX = -88f;
    public float maxX = 66f;

    [Header("Line Materials")]
    [SerializeField] private Material validMaterial;
    [SerializeField] private Material invalidMaterial;

    private BoxCollider boxCollider;
    private LineRenderer lineRenderer;
    private bool isOutOfBounds = false;
    public bool IsOutOfBounds => isOutOfBounds;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        ApplyMaterial();
    }

    void Update()
    {
        if (boxCollider == null) return;

        Bounds b = boxCollider.bounds;
        bool outOfBounds = b.min.x < minX || b.max.x > maxX;

        if (outOfBounds != isOutOfBounds)
        {
            isOutOfBounds = outOfBounds;
            ApplyMaterial();
        }
    }

    private void ApplyMaterial()
    {
        if (lineRenderer == null) return;
        Material m = isOutOfBounds ? invalidMaterial : validMaterial;
        if (m != null) lineRenderer.sharedMaterial = m;
    }
}