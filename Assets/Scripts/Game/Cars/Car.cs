using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Car Model")]
    [SerializeField] private CarModel model;
    
    private float damage;
    private float moveSpeed;

    private void Start()
    {
        damage = model.getBaseDamage();
        moveSpeed = model.getBaseSpeed();
    }

    public int GetCost()
    {
        return model.cost;
    }

    public float GetCooldown()
    {
        return model.cooldown;
    }

    public string GetName()
    {
        return model.modelName;
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
            frog.Damage((int)damage);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
