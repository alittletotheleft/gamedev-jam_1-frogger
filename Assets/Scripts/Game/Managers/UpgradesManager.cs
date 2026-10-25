using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private List<CommunityStat> stats;
    [SerializeField] private GameObject[] upgradeButtons;
    private List<CommunityStat> statsBuffer;

    private void Start()
    {
        statsBuffer = new List<CommunityStat>(stats);
        CheckStats();
        ShuffleStats();
    }

    public void InitializeStats()
    {
        foreach (CommunityStat stat in statsBuffer)
        {
            stat.Level = stat.startingLevel;
        }
    }

    public void InitializeUpgrades()
    {
        ShuffleStats();

        if (statsBuffer.Count > 1)
        {
            upgradeButtons[1].SetActive(true);
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                UpgradeButton upgrade = upgradeButtons[i].GetComponent<UpgradeButton>();
                upgrade.SetStat(statsBuffer[i]);
            }
        }
        else
        {
            upgradeButtons[1].SetActive(false);
            UpgradeButton upgrade = upgradeButtons[0].GetComponent<UpgradeButton>();
            upgrade.SetStat(statsBuffer[0]);
        }

    }

    private void CheckStats()
    {
        for (int i = statsBuffer.Count - 1; i >= 0; i--)
        {
            if (statsBuffer[i].IsMaxed())
            {
                statsBuffer.RemoveAt(i);
            }
        }
    }

    private void ShuffleStats()
    {
        // Debug.Log($"Original: " + string.Join(", ", (object[])stats));
        for (int i = 0; i < statsBuffer.Count - 1; i++)
        {
            int index = Random.Range(0, statsBuffer.Count);
            CommunityStat temp = statsBuffer[i];
            statsBuffer[i] = statsBuffer[index];
            statsBuffer[index] = temp;
        }
        // Debug.Log($"Shuffled: " + string.Join(", ", (object[])stats));
    }
}