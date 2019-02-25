using UnityEngine;

[CreateAssetMenu(fileName = "New QuestObjectDescription", menuName = "Quest Object Description", order = 52)]
public class QuestObjectDescription : ScriptableObject
{
    [Header("Quest Object Prefab")]
    public QuestObject Prefab;
    
    [Header("UI Icon")]
    public Sprite Icon;
}
