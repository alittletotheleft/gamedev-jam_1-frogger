using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

[Serializable]
public struct FrogGroup
{
    public Frog frog;
    public int minimumCount;
    // public int maximumCount;
    // public bool isRandomCount;
}

[Serializable]
public struct FrogWave
{
    public FrogGroup[] frogGroups;
}

public class FrogsManager : MonoBehaviour
{
    // [SerializeField] private FrogWave[] frogWaves;
    [SerializeField] private GameObject[] testFrogs;
    [SerializeField] private Transform frogSpawnParent;
    [SerializeField] private Transform frogSpawnPositionStart;
    [SerializeField] private Transform frogSpawnPositionEnd;


    private void Start()
    {
        StartFrogSpawning();    
    }

    public void StartGame()
    {
        
    }

    public void StopGame()
    {
        
    }

    private void StartFrogSpawning()
    {
        foreach (GameObject frog in testFrogs)
        {
            StartCoroutine(FrogSpawning(frog, 5, 1f));
        }
    }

    private void StopFrogSpawning()
    {
        StopAllCoroutines();
    }

    private IEnumerator FrogSpawning(GameObject frog, int count, float cooldown)
    {
        for (int it = 0; it < count; it++)
        {
            GameObject newFrog = Instantiate(
                frog, 
                Vector3.Lerp(
                    frogSpawnPositionStart.position, 
                    frogSpawnPositionEnd.position, 
                    Random.value
                ),
                frog.transform.rotation
            );
            newFrog.transform.parent = frogSpawnParent;

            yield return new WaitForSeconds(cooldown);
        }
    }
}
