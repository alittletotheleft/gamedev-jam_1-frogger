using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [Header("Death Puddle")]
    [SerializeField] private Renderer bloodPuddlePrefab;
    [SerializeField] private Renderer bodyRenderer;

    [Header("Audio")]
    [SerializeField] private AudioClip[] ows;
    [Range(0f,1f)]
    [SerializeField] private float owVol = 0.25f;
    [SerializeField] private AudioClip[] squishes;
    [Range(0f,1f)]
    [SerializeField] private float squishVol = 0.25f;

    private const float PuddleLifetime = 4f;
    private const float NormalFrogScale = 1.25f;
    private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
    
    private FrogHop frogHop;
    private Transform facing;
    private bool isHurtOnCooldown;
    private int health = 1;
    private int maxHealth = 1;
    private int damage = 0;
    private int rewardValue;
    private bool isDestroyed;

    private void Start()
    {
        frogHop = GetComponent<FrogHop>();
        facing = transform.Find("Facing");
        
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
        if (isDestroyed || isHurtOnCooldown) return;
        health -= damaged;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) 
        {
            DestroySelf();
            return;
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

        SpawnBloodPuddle();

        gameState.FrogsKilled++;

        AudioSource.PlayClipAtPoint(ows[Random.Range(0, ows.Length)], Camera.main.transform.position, owVol);
        AudioSource.PlayClipAtPoint(squishes[Random.Range(0, squishes.Length)], Camera.main.transform.position, squishVol);
        

        float multiplier = 1f + (0.20f * money.Level);
        int earned = Mathf.RoundToInt(rewardValue * multiplier);
        
        if (rewardValue > 0 && earned < 1)
        {
            earned = 1;
        }

        gameState.Money += earned;

        onFrogDestroy.Invoke();
        Destroy(gameObject);
    }

    private void SpawnBloodPuddle()
    {
        if (bloodPuddlePrefab == null || bodyRenderer == null) return;

        // The frog root stays on the ground while the model animates its hop.
        Vector3 position = transform.position + Vector3.up * bloodPuddlePrefab.transform.position.y;
        Quaternion rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.up)
                              * bloodPuddlePrefab.transform.rotation;
        Renderer puddle = Instantiate(bloodPuddlePrefab, position, rotation); // spawn
        float scale = facing != null ? Mathf.Abs(facing.lossyScale.x) / NormalFrogScale : 1f;
        puddle.transform.localScale = bloodPuddlePrefab.transform.localScale * scale; // scale

        var properties = new MaterialPropertyBlock();
        puddle.GetPropertyBlock(properties); // get the current properties of the puddle
        // set the color of the puddle to match the frog's body color
        properties.SetColor(BaseColorId, bodyRenderer.sharedMaterial.GetColor(BaseColorId));
        puddle.SetPropertyBlock(properties);

        // destroy the puddle after the specified lifetime
        Destroy(puddle.gameObject, PuddleLifetime);
    }
    
    public void ReachPlayer()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        onFrogDestroy.Invoke();
        Destroy(gameObject);
    }
}
