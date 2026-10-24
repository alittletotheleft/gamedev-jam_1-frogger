using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    [Header("Car Model")]
    [SerializeField] private CarModel model;
    
    private float damage;
    private float moveSpeed;
    private Action moveCar;
    
    // Trajectory variables
    private int pointCounts = 0;
    private Vector3[] positions;
    private int currentPoint;

    private void Start()
    {
        damage = model.GetBaseDamage();
        moveSpeed = model.GetBaseSpeed();
        
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
                model.GetRotationSpeed() * Time.deltaTime);
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

}
