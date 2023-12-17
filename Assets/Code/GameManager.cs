using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int levelIndex;
    public int fakeLevelIndex;
    private LevelSpawner levelSpawner;

    private void Awake()
    {
        levelSpawner = FindObjectOfType<LevelSpawner>();
        levelIndex = PlayerPrefs.GetInt("SceneIndex");
        fakeLevelIndex = PlayerPrefs.GetInt("FakeLevelIndex");

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
            Debug.Log("Last level, returning to first level...");
            levelIndex = 0;
        }

        PlayerPrefs.SetInt("FakeLevelIndex", fakeLevelIndex);
        PlayerPrefs.SetInt("SceneIndex", levelIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}