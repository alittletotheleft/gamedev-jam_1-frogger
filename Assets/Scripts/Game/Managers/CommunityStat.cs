using UnityEngine;

[CreateAssetMenu(fileName = "CommStat", menuName = "ScriptableObjects/Community Stat")]
public class CommunityStat : ScriptableObject
{
    [SerializeField] private int maxLevel = 7;
    [SerializeField] private string title = "Title";
    [TextArea]
    [SerializeField] string description = "Description";
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

    public bool IsMaxed()
    {
        return level == maxLevel;
    }

    public override string ToString()
    {
        return title;
    }
}