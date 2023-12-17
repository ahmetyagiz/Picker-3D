using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int levelIndex;
    public int fakeLevelIndex;
    private LevelSpawner levelSpawner;

    public int tempInt;
    public bool randomLevels;

    private void Awake()
    {
        levelSpawner = FindObjectOfType<LevelSpawner>();
        levelIndex = PlayerPrefs.GetInt("SceneIndex");
        fakeLevelIndex = PlayerPrefs.GetInt("FakeLevelIndex");
        randomLevels = PlayerPrefs.GetInt("RandomLevels") == 1;

        if (fakeLevelIndex == 0)
        {
            PlayerPrefs.SetInt("FakeLevelIndex", 1);
            fakeLevelIndex = 1;
        }
    }
    public void SceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void NextScene()
    {
        levelIndex++;
        fakeLevelIndex++;

        if (levelIndex == levelSpawner.levelData.levelPrefabs.Length)
        {
            randomLevels = true;
            PlayerPrefs.SetInt("RandomLevels", randomLevels ? 1 : 0);
        }

        if (randomLevels)
        {
            tempInt = Random.Range(0, levelSpawner.levelData.levelPrefabs.Length);

            while (levelIndex == tempInt)
            {
                tempInt = Random.Range(0, levelSpawner.levelData.levelPrefabs.Length);
            }
            levelIndex = tempInt;
        }

        Debug.Log("Selected scene index: " + levelIndex);

        PlayerPrefs.SetInt("FakeLevelIndex", fakeLevelIndex);
        PlayerPrefs.SetInt("SceneIndex", levelIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}