using UnityEngine;

[CreateAssetMenu(fileName = "CommStat", menuName = "ScriptableObjects/Community Stat")]
public class CommunityStat : ScriptableObject
{
    public int startingLevel = 1;
    public int maxLevel = 7;
    public string title = "Title";
    [TextArea]
    public string description = "Description";
    private int level;
    
    public int Level
    {
        get => level;
        set
        {
            level = Mathf.Clamp(value, 1, maxLevel);
        }
    }

    public bool IsMaxed()
    {
        return level == maxLevel;
    }

    public override string ToString()
    {
        return title;
    }
}