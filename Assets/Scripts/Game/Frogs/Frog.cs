using UnityEngine;

public class Frog : MonoBehaviour, IDamageable
{
    [SerializeField] private int baseHealth = 10;
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private float baseMoveSpeed = 10f;
    private int health;
    private int damage;
    private float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = baseHealth;
        damage = baseDamage;
        moveSpeed = baseMoveSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void Damage(int damage)
    {
        
    }
}
