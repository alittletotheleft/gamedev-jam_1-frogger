using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light sun;

    [Header("Time")]
    [Tooltip("Real-time minutes for one full 24h cycle.")]
    [SerializeField] private float cycleDurationMinutes = 6f;
    [Tooltip("Time of day at start, in hours (0-24). 0 = midnight, 6 = sunrise, 12 = noon, 18 = sunset.")]
    [Range(0f, 24f)]
    [SerializeField] private float startHour = 8f;
    [SerializeField] private bool paused = false;

    [Header("Sun")]
    [Tooltip("Compass direction the sun travels along (Y rotation).")]
    [SerializeField] private float sunYaw = 170f;

    [Header("Over the day (left = midnight, right = next midnight)")]
    [SerializeField] private Gradient sunColor = DefaultSunColor();
    [SerializeField] private AnimationCurve sunIntensity = DefaultIntensity();
    [SerializeField] private bool affectAmbient = true;
    [SerializeField] private Gradient ambientColor = DefaultAmbientColor();

    // 0..1 where 0 = midnight, 0.25 = 6:00, 0.5 = noon, 0.75 = 18:00
    private float normalizedTime;

    public float CurrentHour => normalizedTime * 24f;
    public bool IsNight => normalizedTime < 0.25f || normalizedTime > 0.75f;

    private void Start()
    {
        normalizedTime = Mathf.Repeat(startHour / 24f, 1f);
        Apply();
    }

    private void Update()
    {
        if (!paused && cycleDurationMinutes > 0f)
        {
            normalizedTime = Mathf.Repeat(normalizedTime + Time.deltaTime / (cycleDurationMinutes * 60f), 1f);
        }
        Apply();
    }

    public void SetHour(float hour)
    {
        normalizedTime = Mathf.Repeat(hour / 24f, 1f);
        Apply();
    }

    public void SetCycleDuration(float minutes)
    {
        cycleDurationMinutes = minutes;
    }

    public void SetPaused(bool value)
    {
        paused = value;
    }

    private void Apply()
    {
        if (sun != null)
        {
            // 0.25 -> 0 deg (horizon, rising), 0.5 -> 90 deg (overhead), 0.75 -> 180 deg (horizon, setting)
            float angle = normalizedTime * 360f - 90f;
            sun.transform.rotation = Quaternion.Euler(angle, sunYaw, 0f);
            sun.color = sunColor.Evaluate(normalizedTime);
            sun.intensity = sunIntensity.Evaluate(normalizedTime);
        }

        if (affectAmbient)
        {
            RenderSettings.ambientLight = ambientColor.Evaluate(normalizedTime);
        }
    }

    private void OnValidate()
    {
        cycleDurationMinutes = Mathf.Max(0.01f, cycleDurationMinutes);
    }

    private static Gradient DefaultSunColor()
    {
        var g = new Gradient();
        g.SetKeys(
            new[]
            {
                new GradientColorKey(new Color(0.35f, 0.45f, 0.75f), 0f),    // midnight
                new GradientColorKey(new Color(1f, 0.55f, 0.3f), 0.25f),     // sunrise
                new GradientColorKey(new Color(1f, 0.96f, 0.88f), 0.5f),     // noon
                new GradientColorKey(new Color(1f, 0.45f, 0.25f), 0.75f),    // sunset
                new GradientColorKey(new Color(0.35f, 0.45f, 0.75f), 1f)     // midnight
            },
            new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
        return g;
    }

    private static AnimationCurve DefaultIntensity()
    {
        return new AnimationCurve(
            new Keyframe(0f, 0.05f),
            new Keyframe(0.22f, 0.1f),
            new Keyframe(0.3f, 0.9f),
            new Keyframe(0.5f, 1.2f),
            new Keyframe(0.7f, 0.9f),
            new Keyframe(0.78f, 0.1f),
            new Keyframe(1f, 0.05f));
    }

    private static Gradient DefaultAmbientColor()
    {
        var g = new Gradient();
        g.SetKeys(
            new[]
            {
                new GradientColorKey(new Color(0.08f, 0.1f, 0.2f), 0f),
                new GradientColorKey(new Color(0.55f, 0.4f, 0.4f), 0.25f),
                new GradientColorKey(new Color(0.6f, 0.65f, 0.7f), 0.5f),
                new GradientColorKey(new Color(0.5f, 0.35f, 0.35f), 0.75f),
                new GradientColorKey(new Color(0.08f, 0.1f, 0.2f), 1f)
            },
            new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) });
        return g;
    }
}