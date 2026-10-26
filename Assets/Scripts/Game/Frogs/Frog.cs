using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FrogHop))]
public class Frog : MonoBehaviour, IDamageable
{
    [SerializeField] private GameState gameState;
    [SerializeField] private CommunityStat money;
    [Space]
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
    private bool isDestroyed;

    private void Start()
    {
        frogHop = GetComponent<FrogHop>();
        
        health = frogType.GetComputedHealth(gameState.FrogLevel);
        maxHealth = health;
        damage = frogType.GetComputedDamage(gameState.FrogLevel);
        frogHop.SetStepSize(frogType.GetComputedSpeed(gameState.FrogLevel));
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
        if (isDestroyed) return;
        isDestroyed = true;

        gameState.FrogsKilled++;
        
        gameState.Money += Mathf.FloorToInt(rewardValue * 0.50f * (((float)money.Level + 1)/ (money.MaxLevel + 1)));
        onFrogDestroy.Invoke();
        Destroy(gameObject);
    }
    
    public void ReachPlayer()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        onFrogDestroy.Invoke();
        Destroy(gameObject);
    }
}
