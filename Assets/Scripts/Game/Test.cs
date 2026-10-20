using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAAAAAAAAAAA");
        if (other.CompareTag("Frog"))
        {
            Debug.Log("Fuuuuuuuuuuuckkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
        }
    }
}
