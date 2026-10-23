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
        FrogWave wave = frogWaves[gameState.Waves-1];
        int total = 0;

        for (int i = 0; i < wave.frogGroups.Length; i++)
        {
            FrogGroup group = wave.frogGroups[i];
            int count = group.minimumCount;

            StartCoroutine(FrogSpawning(group.frog, count, group.spawnCooldown));

            total += count;
        }
    }

    public void StopFrogSpawning()
    {
        StopAllCoroutines();
    }

    private IEnumerator FrogSpawning(GameObject frog, int count, float cooldown)
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

            yield return new WaitForSeconds(cooldown);
        }
    }
}
