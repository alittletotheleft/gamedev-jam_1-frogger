using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{   
    [SerializeField] private Event onUpgradeSelect;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text levelText;
    private CommunityStat stat;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (stat.Level < stat.maxLevel)
            stat.Level++;
        onUpgradeSelect.Invoke();
    }

    public void SetStat(CommunityStat stat)
    {
        this.stat = stat;
        titleText.SetText(stat.title);
        descriptionText.SetText(stat.description);
        if (stat.Level+1 == stat.maxLevel)
        {
            levelText.SetText($"(Lv. {stat.Level} -> Lv. MAX)");
        }
        else
        {
            levelText.SetText($"(Lv. {stat.Level} -> Lv. {stat.Level})");
        }
    } 
}