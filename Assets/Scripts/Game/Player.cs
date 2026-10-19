using System.Text.Encodings.Web;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject testCar;
    private LayerMask roadMask;

    private void Start()
    {
        roadMask = LayerMask.GetMask("Road");
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DoSomeRaycastingShit();
        }
    }

    private void DoSomeRaycastingShit()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 2f);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, roadMask))
        {
            // Debug.Log($"Point: {hit.point}");
            Instantiate(testCar, hit.point, testCar.transform.rotation);
        }
    }
}
