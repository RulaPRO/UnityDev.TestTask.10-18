using UnityEngine;

[CreateAssetMenu(fileName = "New GamePrefabs", menuName = "Game Prefabs", order = 51)]
public class GamePrefabs : ScriptableObject
{
    [Header("Character")]
    public PlayerController Player;

    [Header("Quests Objects")]
    public QuestObjectDescription[] QuestsObjects;
    
    [Header("Particle Effect")]
    public GameObject Success;
    public GameObject Fail;
}
