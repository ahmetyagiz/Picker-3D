using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Custom/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public GameObject[] levelPrefabs;
}
