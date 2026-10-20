using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }
}
