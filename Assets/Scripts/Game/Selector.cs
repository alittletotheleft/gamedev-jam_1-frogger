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
            ShowGhost();
        }
    }

    public void OnSelect(GameObject car)
    {
        if (isPlacing)
        {
            selectedCar = null;
            isPlacing = false;
        }
        else
        {
            selectedCar = car;
            isPlacing = true;
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed && isPlacing)
        {
            PlaceCar();
        }
    }

    private void ShowGhost()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, roadMask))
        {
            testCarGhost.SetActive(true);
            testCarGhost.transform.position = hit.point;
        }
        else
        {
            testCarGhost.SetActive(false);
        }
    }

    private void PlaceCar()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 2f);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, roadMask))
        {
            Instantiate(testCar, hit.point, testCar.transform.rotation);
        }
    }
}
