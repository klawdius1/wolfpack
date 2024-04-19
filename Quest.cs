using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest")]
public class Quest : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public string progress;
    public int reward;
}
