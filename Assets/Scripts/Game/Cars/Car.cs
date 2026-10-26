using UnityEngine;
using System;
using System.Collections.Generic;
// using System.Diagnostics;

public class Car : MonoBehaviour
{
    [Header("Car Model")]
    [SerializeField] private CarModel model;
    [SerializeField] private CommunityStat carSpeed;
    [SerializeField] private CommunityStat carDamage;
    [SerializeField] private CommunityStat carCost;
    [SerializeField] private CommunityStat carCooldown;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private AudioClip bonkSFX;
    [Range(0f,1f)]
    [SerializeField] private float bonkVol;
    [SerializeField] private AudioClip explosionSFX;
    [Range(0f,1f)]
    [SerializeField] private float explosionVol;
    
    private int damage;
    private float moveSpeed;
    private Action moveCar;
    private float rotationSpeed;
    private bool hasExploded;
    
    // Trajectory variables
    private int pointCounts = 0;
    private Vector3[] positions;
    private int currentPoint;
    private Dictionary<int, ModelSpeed> speedChanges;

    private void Start()
    {
        damage = GetComputedDamage();
        moveSpeed = GetComputedSpeed();
        rotationSpeed = model.GetRotationSpeed();
        
        moveCar = model.pathTrajectory == null ? MoveStraight : FollowTrajectory;

        if (model.pathTrajectory == null)
        {
            moveCar = MoveStraight;
        }
        else
        {
            moveCar = FollowTrajectory;
            
            speedChanges = new Dictionary<int, ModelSpeed>();
            foreach (PointSpeed ps in model.pointSpeeds)
                speedChanges[ps.pointIndexApproaching] = ps.speed;
            
            InitializeTracjectory();
        }
    }
    
    private void Update()
    {
        moveCar();
        if (transform.position.z < -160f || transform.position.z > 160f)
        {
            DestroySelf();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Frog"))
        {
            IDamageable frog = other.GetComponentInParent<IDamageable>();
            frog?.Damage(damage);
            Debug.Log($"Frog hit w/ dmg: {damage} at speed: {moveSpeed}");
            AudioSource.PlayClipAtPoint(bonkSFX, Camera.main.transform.position, bonkVol);
        }

        if (other.CompareTag("Car"))
        {
            if (hasExploded) return;
            Car otherCar = other.GetComponentInParent<Car>();
            
            if (otherCar == null || otherCar == this || otherCar.hasExploded) return;
            if (otherCar.GetName() == "Wheel") return;

            otherCar.hasExploded = true;
            hasExploded = true;

            // Large vehicle colisions rewards bigger explosions and damage
            float explosionRadius;
            int explosionDamage;
            if (otherCar.GetSize() == ModelSize.Large || GetSize() == ModelSize.Large)
            {
                explosionRadius = 40f;
                explosionDamage = Mathf.RoundToInt((otherCar.GetComputedDamage() + damage) * 1.40f);
            }
            else
            {
                explosionRadius = 28f;
                explosionDamage = Mathf.RoundToInt((otherCar.GetComputedDamage() + damage) * 1.10f);
            }
            
            Debug.Log($"Car: {damage}, Other: {otherCar.GetComputedDamage()}");

            Vector3 explosionPoint = other.ClosestPoint(new Vector3(transform.position.x, 0, transform.position.z));

            PlayExplosion(transform.position);
            otherCar.PlayExplosion(otherCar.transform.position);

            Collider[] hits = Physics.OverlapSphere(explosionPoint, explosionRadius);
            HashSet<IDamageable> damaged = new HashSet<IDamageable>();

            foreach (Collider hit in hits)
            {
                if (hit.GetComponentInParent<Car>() != null) continue;

                IDamageable frog = hit.GetComponentInParent<IDamageable>();
                if (frog != null && damaged.Add(frog))
                    frog.Damage(explosionDamage);
            }

            Debug.Log($"EXPLOSION!!! {damaged.Count} frogs hit. Radius = {explosionRadius}. Damage = {explosionDamage}");

            DestroySelf();
            if (otherCar != null) otherCar.DestroySelf();
        }
    }

    private void PlayExplosion(Vector3 position)
    {
        if (explosionParticle == null) return;

        ParticleSystem explosion = Instantiate(explosionParticle, position, explosionParticle.transform.rotation);
        if (GetSize() == ModelSize.Large)
            explosion.transform.localScale *= 1.15f;

        ParticleSystem.MainModule main = explosion.main;
        main.stopAction = ParticleSystemStopAction.Destroy;
        explosion.Play();

        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionVol);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void MoveStraight()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void InitializeTracjectory()
    {
        // initialize points
        pointCounts = model.pathTrajectory.positionCount;
        positions = new Vector3[pointCounts];
        model.pathTrajectory.GetPositions(positions);

        // point positions
        for (int i = 0; i < positions.Length; i++)
            positions[i] = transform.TransformPoint(positions[i] + model.pathOffset);
        
        currentPoint = 1;
        
        if (positions.Length < 2)
            moveCar = MoveStraight;
    }

    private void FollowTrajectory()
    {
        
        // dynamic speed
        if (speedChanges.TryGetValue(currentPoint, out ModelSpeed newSpeed))
        {
            moveSpeed = GetComputedSpeed(model.GetSpeed(newSpeed));
            damage = GetComputedDamage(newSpeed); // update correspoinding damage
        }
        
        // movement
        Vector3 targetPoint = positions[currentPoint];
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint,
            moveSpeed * Time.deltaTime);

        // rotation
        Vector3 direction = targetPoint - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        // point indexing
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            if (currentPoint < positions.Length - 1)
                currentPoint++;
            else
                moveCar = MoveStraight; // path finished
        }
    }
    
    public int GetCost()
    {
        return Mathf.FloorToInt(model.cost * (1f - 0.50f * ((float)carCost.Level/carCost.MaxLevel)));
    }

    public float GetCooldown()
    {
        return Mathf.FloorToInt(model.cooldown * (1f - 0.50f * ((float)carCooldown.Level/carCooldown.MaxLevel)));
    }

    public string GetName()
    {
        return model.modelName;
    }
    
    public LineRenderer GetTrajectory()
    {
        return model.pathTrajectory;
    }

    public ModelSize GetSize()
    {
        return model.size;
    }
    
    public int GetComputedDamage()
    {
        return GetComputedDamage(model.speedType);
    }
    
    public int GetComputedDamage(ModelSpeed speed)
    {
        float dmg = model.GetDamage(speed);
        if (model.modelName == "Drunk Driver") dmg *= 0.75f; // i aint making a whole child class just for this
        if (model.modelName == "Wheel") dmg *= 0.4f;
        return Mathf.RoundToInt(dmg * (1 + 0.15f * carDamage.Level));
    }
    
    public float GetComputedSpeed()
    {
        return model.GetBaseSpeed() + (2 * carSpeed.Level);
    }

    public float GetComputedSpeed(float speed)
    {
        return speed + (2 * carSpeed.Level);
    }
    
    public Vector3 GetPathOffset()
    {
        return model.pathOffset;
    }

}
