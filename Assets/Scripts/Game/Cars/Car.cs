using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private int baseHealth = 10;
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private float baseMoveSpeed = 50f;
    private int health;
    private int damage;
    private float moveSpeed;

    private void Start()
    {
        health = baseHealth;
        damage = baseDamage;
        moveSpeed = baseMoveSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    
    // why u not work what the fcuk
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered");
        if (other.CompareTag("Frog"))
        {
            Debug.Log($"Frog entered");
            IDamageable frog = other.GetComponentInParent<IDamageable>();
            frog.Damage(damage);
        }
    }
}
