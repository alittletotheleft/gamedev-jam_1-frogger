using UnityEngine;

public class PlayerBox : MonoBehaviour
{
    [SerializeField] private IntEvent OnFrogEnter; 
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Tag: {other.tag} / CompareTag(\"Frog\"): {other.CompareTag("Frog")}");
        if (other.CompareTag("Frog"))
        {
            Frog frog = other.GetComponent<Frog>();
            OnFrogEnter.Invoke(frog.GetDamage());
            frog.ReachPlayer();
        }
    }
}
