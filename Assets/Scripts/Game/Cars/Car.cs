using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private int baseHealth = 10;
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private float baseMoveSpeed = 50f;
    [SerializeField] private int cost = 100;
    [SerializeField] private float cooldown = 6f;
    private int health;
    private int damage;
    private float moveSpeed;

    private void Start()
    {
        health = baseHealth;
        damage = baseDamage;
        moveSpeed = baseMoveSpeed;
    }

    public int GetCost()
    {
        return cost;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (transform.position.z < -150f || transform.position.z > 150f)
        {
            DestroySelf();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Frog"))
        {
            IDamageable frog = other.GetComponentInParent<IDamageable>();
            frog.Damage(damage);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
