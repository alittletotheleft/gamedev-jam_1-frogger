using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FrogHop))]
public class Frog : MonoBehaviour, IDamageable
{
    [SerializeField] private FrogType frogType;
    [SerializeField] private float hurtCooldown = 0.5f;
    [SerializeField] private Event onFrogDestroy;
    [SerializeField] private HealthBar healthBar;
    
    private FrogHop frogHop;
    private bool isHurtOnCooldown;
    private int health = 1;
    private int maxHealth = 1;
    private int damage = 0;
    private int rewardValue;

    [NonSerialized]
    public int level = 0;

    private void Start()
    {
        frogHop = GetComponent<FrogHop>();
        
        health = frogType.GetComputedHealth(level);
        maxHealth = health;
        damage = frogType.GetComputedDamage(level);
        frogHop.SetStepSize(frogType.GetComputedSpeed(level));
        rewardValue = frogType.baseRewards;
        
        isHurtOnCooldown = false;
        healthBar.SetMaxHealth(maxHealth);
        
        StartCoroutine(HopLoop());
    }

    
    private IEnumerator HopLoop()
    {
        while (true)
        {
            frogHop.Hop(Vector3.right);
            yield return new WaitUntil(() => !frogHop.IsHopping);
            yield return new WaitForSeconds(UnityEngine.Random.Range(frogType.minHopDelay, frogType.maxHopDelay));
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public void Damage(int damaged)
    {
        if (isHurtOnCooldown) return;
        health -= damaged;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) 
        {
            DestroySelf();
        }
        StartCoroutine(StartHurtCooldown());
    }

    private IEnumerator StartHurtCooldown()
    {
        isHurtOnCooldown = true;
        yield return new WaitForSeconds(hurtCooldown);
        isHurtOnCooldown = false;
    }

    private void DestroySelf()
    {
        onFrogDestroy.Invoke();
        Destroy(gameObject);
    }
}
