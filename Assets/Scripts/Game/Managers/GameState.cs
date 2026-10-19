using UnityEngine;

[CreateAssetMenu(fileName="GameState",menuName="ScriptableObjects/Game State")]
public class GameState : ScriptableObject
{
    public bool IsGameRunning { get; set; }
}
