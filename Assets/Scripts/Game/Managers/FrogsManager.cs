using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private int frogLevel = 0;

    public void StartFrogSpawning()
    {
        Debug.Log($"Spawning wave #{gameState.Waves}.");

        FrogWave wave;
        int total = 0;
        if (gameState.Waves > frogWaves.Length)
            wave = frogWaves[^1];
        else
            wave = frogWaves[gameState.Waves - 1];

        for (int i = 0; i < wave.frogGroups.Length; i++)
        {
            FrogGroup group = wave.frogGroups[i];
            List<GameObject> spawnList = group.BuildSpawnList();

            StartCoroutine(FrogSpawning(spawnList, group.startDelay, group.minimumSpawnCooldown, group.maximumSpawnCooldown));
            total += spawnList.Count;
        }

        gameState.WaveCount = total;
        Debug.Log($"Wave #{gameState.Waves} spawned with {gameState.WaveCount} frogs.");
    }
    
    public void StopFrogSpawning()
    {
        Debug.Log($"Stopped spawning wave #{gameState.Waves}.");
        StopAllCoroutines();
    }

    private IEnumerator FrogSpawning(List<GameObject> spawnList, float startDelay, float minCooldown, float maxCooldown)
    {
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        foreach (GameObject frog in spawnList)
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
            newFrog.GetComponent<Frog>().level = frogLevel;

            yield return new WaitForSeconds(Mathf.Lerp(minCooldown, maxCooldown, Random.value));
        }
    }
}
