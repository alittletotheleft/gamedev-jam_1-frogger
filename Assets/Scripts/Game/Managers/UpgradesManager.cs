using UnityEditor.Analytics;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private CommunityStat[] stats;
    [SerializeField] private GameObject[] upgradeButtons;

    private void Start()
    {
        ShuffleStats();
    }

    public void InitializeStats()
    {
        foreach (CommunityStat stat in stats)
        {
            stat.Level = stat.startingLevel;
        }
    }
    
    public void InitializeUpgrades()
    {
        ShuffleStats();
        
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            UpgradeButton upgrade = upgradeButtons[i].GetComponent<UpgradeButton>();
            upgrade.SetStat(stats[i]);
        }
    }

    private void ShuffleStats()
    {
        // Debug.Log($"Original: " + string.Join(", ", (object[])stats));
        for (int i = 0; i < stats.Length-1; i++)
        {
            int index = Random.Range(0, stats.Length);
            CommunityStat temp = stats[i];
            stats[i] = stats[index];
            stats[index] = temp;
        }
        // Debug.Log($"Shuffled: " + string.Join(", ", (object[])stats));
    }
}