using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject carSelectables;
    [SerializeField] private GameObject testCar;
    [SerializeField] private GameObject testCarGhost;
    [SerializeField] private GameObject ghostCar;
    [SerializeField] private Transform trajectoryParent;
    [SerializeField] private int carLevel = 0;

    private LayerMask mask;
    private GameObject selectedCar;
    private bool isPlacing;
    private Action buttonCallback;
    private GameObject trajectoryInstance;
    
    

    private void Start()
    {
        mask = LayerMask.GetMask("CarSelectable");
        selectedCar = null;
        isPlacing = false;
        carSelectables.SetActive(false);
        testCarGhost.SetActive(false);
        buttonCallback = null;
        trajectoryInstance = null;
    }

    private void Update()
    {
        if (isPlacing)
        {
            ShowGhostCar();
        }
    }

    public void OnCarSelect(GameObject carObj, Action callback)
    {
        Car car = carObj.GetComponent<Car>();
        buttonCallback = callback;

        if (isPlacing && selectedCar == carObj)
        {
            UnselectCar();
            return;
        }

        SelectCar(car);
    }

    private void SelectCar(Car car)
    {
        selectedCar = car.gameObject;
        isPlacing = true;
        carSelectables.SetActive(true);
        ShowTrajectory(car);
    }

    private void ShowTrajectory(Car car)
    {
        ClearTrajectory();

        LineRenderer source = car.GetTrajectory();
        if (source == null)
        {
            Debug.Log("No trajectory on model for " + car.name);
            return;
        }

        Transform parent = trajectoryParent != null ? trajectoryParent : transform;
        trajectoryInstance = Instantiate(source.gameObject, parent);
        trajectoryInstance.transform.position = testCarGhost.transform.position;
        trajectoryInstance.transform.rotation = testCarGhost.transform.rotation;
        trajectoryInstance.SetActive(true);

        int ignoreLayer = LayerMask.NameToLayer("Ignore Raycast");
        if (ignoreLayer >= 0) SetLayerRecursive(trajectoryInstance, ignoreLayer);
    }

    private static void SetLayerRecursive(GameObject go, int layer)
    {
        go.layer = layer;
        foreach (Transform child in go.transform) SetLayerRecursive(child.gameObject, layer);
    }

    private void ClearTrajectory()
    {
        if (trajectoryInstance != null) Destroy(trajectoryInstance);
        trajectoryInstance = null;
    }

    private void UnselectCar()
    {
        ClearTrajectory();
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

    private void ShowGhostCar()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore))
        {
            testCarGhost.SetActive(true);
            testCarGhost.transform.position = hit.transform.position;
            testCarGhost.transform.rotation = hit.transform.rotation;

            if (trajectoryInstance != null)
            {
                trajectoryInstance.SetActive(true);
                trajectoryInstance.transform.position = testCarGhost.transform.position;
                trajectoryInstance.transform.rotation = testCarGhost.transform.rotation;
            }
        }
        else
        {
            testCarGhost.SetActive(false);
            if (trajectoryInstance != null) trajectoryInstance.SetActive(false);
        }
    }

    private void PlaceCar()
    {
        if (selectedCar == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore))
            return;
        
        // check if out of bounds
        if (trajectoryInstance != null)
        {
            Trajectory trajectory = trajectoryInstance.GetComponentInChildren<Trajectory>();
            if (trajectory != null && trajectory.IsOutOfBounds)
            {
                return;
            }
        }

        GameObject spawned = Instantiate(selectedCar, hit.transform.position, hit.transform.rotation);
        Car car = spawned.GetComponent<Car>();
        car.level = carLevel;
        gameState.Money -= car.GetCost();

        Action cb = buttonCallback;
        UnselectCar();
        buttonCallback = null;
        cb?.Invoke();
    }
}