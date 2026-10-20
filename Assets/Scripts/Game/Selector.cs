using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject testCar;
    [SerializeField] private GameObject testCarGhost;
    private LayerMask roadMask;
    private GameObject selectedCar;
    private bool isPlacing;

    private void Start()
    {
        roadMask = LayerMask.GetMask("Road");
        selectedCar = null;
        isPlacing = false;
        testCarGhost.SetActive(false);
    }

    private void Update()
    {
        if (isPlacing)
        {
            Test();
        }
    }

    private void Test()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, roadMask))
        {
            testCarGhost.transform.position = hit.point;
            Debug.Log("Tes");
        }
    }

    public void OnSelect(GameObject car)
    {
        Debug.Log("Besbrnthfgn");
        if (isPlacing)
        {
            selectedCar = null;
            isPlacing = false;
            testCarGhost.SetActive(false);
        }
        else
        {
            selectedCar = car;
            isPlacing = true;
            testCarGhost.SetActive(true);
        }
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
