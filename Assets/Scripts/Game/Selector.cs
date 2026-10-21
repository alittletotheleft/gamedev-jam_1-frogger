using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject carSelectables;
    [SerializeField] private GameObject testCar;
    [SerializeField] private GameObject testCarGhost;
    private LayerMask mask;
    private GameObject selectedCar;
    private bool isPlacing;

    private void Start()
    {
        // mask = LayerMask.GetMask("Road");
        mask = LayerMask.GetMask("CarSelectable");
        selectedCar = null;
        isPlacing = false;
        carSelectables.SetActive(false);
        testCarGhost.SetActive(false);
    }

    private void Update()
    {
        if (isPlacing)
        {
            ShowGhost();
        }
    }

    public void OnCarSelect(GameObject car)
    {
        if (isPlacing)
        {
            if (selectedCar == car)
            {
                UnselectCar();
            }
            else
            {
                SelectCar(car);
            }
        }
        else
        {
            SelectCar(car);
        }
    }
    
    private void SelectCar(GameObject car)
    {
        selectedCar = car;
        isPlacing = true;
        carSelectables.SetActive(true);
    }

    private void UnselectCar()
    {
        selectedCar = null;
        isPlacing = false;
        carSelectables.SetActive(false);
        testCarGhost.SetActive(false);
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
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, mask))
        {
            testCarGhost.SetActive(true);
            testCarGhost.transform.position = hit.transform.position;
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

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, mask))
        {
            Instantiate(selectedCar, hit.transform.position, hit.transform.rotation);
            UnselectCar();
        }
    }
}
