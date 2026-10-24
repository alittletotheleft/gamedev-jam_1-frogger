using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class FrogsManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private FrogWave[] frogWaves;
    [SerializeField] private Transform frogSpawnParent;
    [SerializeField] private Transform frogSpawnPositionStart;
    [SerializeField] private Transform frogSpawnPositionEnd;

    public void StartFrogSpawning()
    {
        Debug.Log($"Spawning wave #{gameState.Waves}.");
        
        FrogWave wave;
        int total = 0;
        if (gameState.Waves > frogWaves.Length)
            wave = frogWaves[^1];
        else
            wave = frogWaves[gameState.Waves-1];

        for (int i = 0; i < wave.frogGroups.Length; i++)
        {
            FrogGroup group = wave.frogGroups[i];
            int count = group.minimumSpawnCount;

            StartCoroutine(FrogSpawning(group.frog, count, group.minimumSpawnCooldown, group.maximumSpawnCooldown));
            total += count;
        }

        gameState.WaveCount = total;
        Debug.Log($"Wave #{gameState.Waves} spawned with {gameState.WaveCount} frogs.");
    }

    public void StopFrogSpawning()
    {
        Debug.Log($"Stopped spawning wave #{gameState.Waves}.");
        StopAllCoroutines();
    }

    private IEnumerator FrogSpawning(GameObject frog, int count, float minCooldown, float maxCooldown)
    {
        for (int i = 0; i < count; i++)
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
            
            float cooldown = Mathf.Lerp(minCooldown, maxCooldown, Random.value);
            yield return new WaitForSeconds(cooldown);
        }
    }
}
