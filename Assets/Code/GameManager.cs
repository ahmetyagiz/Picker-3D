using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int levelIndex;
    [SerializeField] private LevelSpawner levelSpawner;

    private void Start()
    {
        levelSpawner = FindObjectOfType<LevelSpawner>();
        levelIndex = PlayerPrefs.GetInt("SceneIndex");
    }
    public void SceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        levelIndex++;

        if (levelIndex == levelSpawner.levelData.levelPrefabs.Length)
        {
            Debug.Log("Last level, returning to first level...");
            levelIndex = 0;
        }
        
        PlayerPrefs.SetInt("SceneIndex", levelIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}