using UnityEngine;

[CreateAssetMenu(fileName = "FrogGroup", menuName = "ScriptableObjects/Frogs/Frog Group")]
public class FrogGroup : ScriptableObject
{
    public GameObject frog;
    public int minimumCount;
    public int maximumCount;
    public bool isRandomCount;
    public int spawnCooldown;
}