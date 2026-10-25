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
        if (!stat.IsMaxed())
            stat.Level++;
        onUpgradeSelect.Invoke();
    }

    public void SetStat(CommunityStat stat)
    {
        this.stat = stat;
        titleText.SetText(stat.Title);
        descriptionText.SetText(stat.Description);
        if (stat.IsMaxed())
        {
            levelText.SetText($"(Lv. {stat.Level} -> Lv. MAX)");
        }
        else
        {
            levelText.SetText($"(Lv. {stat.Level} -> Lv. {stat.Level+1})");
        }
    } 
}