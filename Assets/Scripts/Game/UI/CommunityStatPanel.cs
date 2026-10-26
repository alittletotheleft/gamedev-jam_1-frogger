using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommunityStatPanel : MonoBehaviour
{
    [SerializeField] private GameState gameState; 
    [SerializeField] private CommunityStat stat; 
    [SerializeField] private TMP_Text titleText; 
    [SerializeField] private TMP_Text shortDescriptionText; 
    [SerializeField] private TMP_Text levelsText;

    private void Start()
    {
        titleText.SetText(stat.Title);
        shortDescriptionText.SetText($"({stat.ShortDescription})");
    }
    
    private void Update()
    {
        if (gameState.IsGameRunning)
        {
            levelsText.SetText($"Lv. {stat.Level} / {stat.MaxLevel}");
        }
    }
}
