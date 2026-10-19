using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CarButton : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private UnityEvent<GameObject> onSelect;
    private Button button;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Test);
    }
    
    private void Test()
    {
        onSelect.Invoke(car);
    }
}
