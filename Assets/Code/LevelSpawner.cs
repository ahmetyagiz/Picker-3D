using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public LevelData levelData;
    public int selectedSceneIndex;
    private void Start()
    {
        selectedSceneIndex = PlayerPrefs.GetInt("SceneIndex");
        Instantiate(levelData.levelPrefabs[selectedSceneIndex]);
    }
}
