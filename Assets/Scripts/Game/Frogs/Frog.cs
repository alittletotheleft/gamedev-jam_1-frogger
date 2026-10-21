using System.Collections;
using UnityEngine;

public class Frog : MonoBehaviour, IDamageable
{
    [SerializeField] private int baseHealth = 10;
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private float baseMoveSpeed = 10f;
    [SerializeField] private int baseMoney = 20;
    [SerializeField] private float hurtCooldown = 0.5f;
    private bool isHurtOnCooldown;
    private int health;
    private int damage;
    private float moveSpeed;
    private int money;

    private void Start()
    {
        health = baseHealth;
        damage = baseDamage;
        moveSpeed = baseMoveSpeed;
        money = baseMoney;
        isHurtOnCooldown = false;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void Damage(int damage)
    {
        if (isHurtOnCooldown) return;
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(StartHurtCooldown());
    }

    private IEnumerator StartHurtCooldown()
    {
        isHurtOnCooldown = true;
        yield return new WaitForSeconds(hurtCooldown);
        isHurtOnCooldown = false;
    }
}
