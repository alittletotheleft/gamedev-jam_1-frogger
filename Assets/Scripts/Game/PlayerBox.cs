using UnityEngine;

public class PlayerBox : MonoBehaviour
{
    [SerializeField] private Event OnFrogEnter; 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Frog"))
        {
            Destroy(other);
            OnFrogEnter.Invoke();
        }
    }
}
