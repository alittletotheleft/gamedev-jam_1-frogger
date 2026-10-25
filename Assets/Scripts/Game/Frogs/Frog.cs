using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FrogHop))]
public class Frog : MonoBehaviour, IDamageable
{
    [SerializeField] private FrogType frogType;
    [SerializeField] private float hurtCooldown = 0.5f;
    [SerializeField] private Event onFrogDestroy;
    private FrogHop frogHop;
    private bool isHurtOnCooldown;
    private int health = 1;
    private int damage = 0;
    private float moveSpeed = 8f;
    private int rewardValue;

    [NonSerialized]
    public int level = 0;

    private void Start()
    {
        frogHop = GetComponent<FrogHop>();
        
        health = frogType.GetComputedHealth(level);
        damage = frogType.GetComputedDamage(level);
        moveSpeed = frogType.GetComputedSpeed(level);
        rewardValue = frogType.baseRewards;
        
        isHurtOnCooldown = false;
    }

    // private void Update()
    // {
    //     transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    // }

    public int GetDamage()
    {
        return damage;
    }

    public void Damage(int damaged)
    {
        if (isHurtOnCooldown) return;
        health -= damaged;
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
