using UnityEngine;
using System;
using System.Collections.Generic;

public class Car : MonoBehaviour
{
    [Header("Car Model")]
    [SerializeField] private CarModel model;
    
    private float damage;
    private float moveSpeed;
    private Action moveCar;
    private float rotationSpeed;
    private bool hasExploded;
    
    // Trajectory variables
    private int pointCounts = 0;
    private Vector3[] positions;
    private int currentPoint;

    private void Start()
    {
        damage = model.GetBaseDamage();
        moveSpeed = model.GetBaseSpeed();
        rotationSpeed = model.GetRotationSpeed();
        
        moveCar = model.pathTrajectory == null ? MoveStraight : FollowTrajectory;

        if (model.pathTrajectory == null)
        {
            moveCar = MoveStraight;
        }
        else
        {
            moveCar = FollowTrajectory;
            InitializeTracjectory();
        }
    }
    
    private void Update()
    {
        moveCar();
        if (transform.position.z < -150f || transform.position.z > 85f)
        {
            DestroySelf();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Frog"))
        {
            IDamageable frog = other.GetComponentInParent<IDamageable>();
            frog?.Damage((int)damage);
        }

        if (other.CompareTag("Car"))
        {
            if (hasExploded) return;
            hasExploded = true;

            Car otherCar = other.GetComponentInParent<Car>();
            if (otherCar != null) otherCar.hasExploded = true;

            // Large vehicle colisions rewards bigger explosions and damage
            float explosionRadius, explosionDamage;
            if (otherCar.GetSize() == ModelSize.Large || GetSize() == ModelSize.Large)
            {
                explosionRadius = 36f;
                explosionDamage = (otherCar.GetBaseDamage() + model.GetBaseDamage()) * 1.33f;
            }
            else
            {
                explosionRadius = 24f;
                explosionDamage = (otherCar.GetBaseDamage() + model.GetBaseDamage());
            }

            Vector3 explosionPoint = other.ClosestPoint(new Vector3(transform.position.x, 0, transform.position.z));

            // Displaying the explosion
            // GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // marker.transform.position = explosionPoint;
            // marker.transform.localScale = Vector3.one * explosionRadius * 2f;
            // Destroy(marker.GetComponent<Collider>());
            // Destroy(marker, 10f);

            Collider[] hits = Physics.OverlapSphere(explosionPoint, explosionRadius);
            HashSet<IDamageable> damaged = new HashSet<IDamageable>();

            foreach (Collider hit in hits)
            {
                if (hit.GetComponentInParent<Car>() != null) continue;

                IDamageable frog = hit.GetComponentInParent<IDamageable>();
                if (frog != null && damaged.Add(frog))
                    frog.Damage((int)explosionDamage);
            }

            Debug.Log($"EXPLOSION!!! {damaged.Count} frogs hit. Radius = {explosionRadius}. Damage = {explosionDamage}");

            DestroySelf();
            if (otherCar != null) otherCar.DestroySelf();
        }
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
            positions[i] = transform.TransformPoint(positions[i]);
        
        currentPoint = 0;
    }

    private void FollowTrajectory()
    {
        Vector3 targetPoint = positions[currentPoint];

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint,
            moveSpeed * Time.deltaTime);

        Vector3 direction = targetPoint - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        if (currentPoint < positions.Length - 1 &&
            Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            currentPoint++;
        }
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
    
    public LineRenderer GetTrajectory()
    {
        return model.pathTrajectory;
    }

    public float GetBaseDamage()
    {
        return model.GetBaseDamage();
    }

    public ModelSize GetSize()
    {
        return model.size;
    }

}
