using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FrogHop : MonoBehaviour
{
    [SerializeField] private float stepSize = 4.5f;

    private const float TakeoffTime = 18f / 60f;
    private const float LandTime = 48f / 60f;
    private const float ClipLength = 60f / 60f;

    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private Animator animator;
    private Transform facing;
    private Rigidbody rb;

    public bool IsHopping { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        facing = transform.Find("Facing");
        rb = GetComponent<Rigidbody>();

        if (facing == null)
        {
            Debug.LogError($"{name}: no child named 'Facing' found.", this);
        }
    }

    public void Hop(Vector3 direction)
    {
        if (IsHopping || direction == Vector3.zero) return;
        StartCoroutine(HopRoutine(direction.normalized));
    }

    private IEnumerator HopRoutine(Vector3 direction)
    {
        IsHopping = true;
        facing.rotation = Quaternion.LookRotation(direction, Vector3.up);
        animator.SetTrigger(JumpHash);

        yield return new WaitForSeconds(TakeoffTime);

        var start = rb != null ? rb.position : transform.position;
        var end = start + direction * stepSize;
        var duration = LandTime - TakeoffTime;
        var wait = rb != null ? new WaitForFixedUpdate() : null;

        for (var t = 0f; t < duration; t += rb != null ? Time.fixedDeltaTime : Time.deltaTime)
        {
            MoveTo(Vector3.Lerp(start, end, t / duration));
            yield return wait;
        }

        MoveTo(end);

        yield return new WaitForSeconds(ClipLength - LandTime);
        IsHopping = false;
    }

    private void MoveTo(Vector3 position)
    {
        if (rb != null)
        {
            rb.MovePosition(position);
        }
        else
        {
            transform.position = position;
        }
    }
}