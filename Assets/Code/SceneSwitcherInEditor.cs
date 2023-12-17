using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherInEditor: MonoBehaviour
{
    public string currentInput = "";
    public float inputDelay = 1.5f; // 1.5 saniyelik gecikme s�resi
    public float lastInputTime = 0f;
    public LevelData levelData;

    void Update()
    {
        // Klavyeden herhangi bir tu�a bas�ld���nda inputu al
        if (Input.anyKeyDown)
        {
            // E�er bir rakam ise, inputa ekle
            if (Input.inputString.Length == 1 && char.IsDigit(Input.inputString[0]))
            {
                currentInput += Input.inputString[0];
                lastInputTime = Time.time; // Son input zaman�n� g�ncelle
            }
            // E�er herhangi bir di�er tu�a bas�ld�ysa, inputu s�f�rla
            else
            {
                currentInput = "";
            }
        }

        // Son inputun �zerinden belirli bir s�re ge�tiyse, sahneye ge�i� yap
        if (currentInput != "" && Time.time - lastInputTime >= inputDelay)
        {
            SwitchToScene();
        }
    }

    void SwitchToScene()
    {
        PlayerPrefs.SetInt("RandomLevels", 0);

        int sceneIndex;
        int mod = 0;

        if (int.TryParse(currentInput, out sceneIndex))
        {
            int sceneCount = levelData.levelPrefabs.Length;

            //Scene index pozitifse
            if (sceneIndex - 1 >= 0)
            {
                PlayerPrefs.SetInt("SceneIndex", sceneIndex - 1);

                if (sceneIndex > sceneCount)
                {
                    mod = sceneIndex % sceneCount;

                    PlayerPrefs.SetInt("SceneIndex", mod - 1);
                }
            }
            else
            {
                Debug.LogError("Hatal� sahne index'i!, ayn� sahne ba�lat�l�yor...");
            }
        }
        else
        {
            Debug.LogError("Ge�ersiz input! Sadece rakamlar� girin.");
        }

        // Sahneye ge�tikten sonra inputu s�f�rla
        currentInput = "";

        SceneRestart();
    }

    public void SceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
