using UnityEngine;

[CreateAssetMenu(fileName = "CommStat", menuName = "ScriptableObjects/Community Stat")]
public class CommunityStat : ScriptableObject
{
    [SerializeField] private int maxLevel = 15;
    [SerializeField] private string title = "Title";
    [TextArea]
    [SerializeField] string description = "Description";
    [SerializeField] private string shortDescription = "(Stat)";
    private int level = 0;

    public int Level
    {
        get => level;
        set
        {
            level = Mathf.Clamp(value, 0, maxLevel);
        }
    }
    public int MaxLevel { get { return maxLevel; } }
    public string Title { get { return title; } }
    public string Description { get { return description; } }
    public string ShortDescription { get { return shortDescription; } }

    public bool IsMaxed()
    {
        return level == maxLevel;
    }

    public override string ToString()
    {
        return title;
    }
}