using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherInEditor: MonoBehaviour
{
    public string currentInput = "";
    public float inputDelay = 1.5f; // 1.5 saniyelik gecikme süresi
    public float lastInputTime = 0f;
    public LevelData levelData;

    void Update()
    {
        // Klavyeden herhangi bir tuþa basýldýðýnda inputu al
        if (Input.anyKeyDown)
        {
            // Eðer bir rakam ise, inputa ekle
            if (Input.inputString.Length == 1 && char.IsDigit(Input.inputString[0]))
            {
                currentInput += Input.inputString[0];
                lastInputTime = Time.time; // Son input zamanýný güncelle
            }
            // Eðer herhangi bir diðer tuþa basýldýysa, inputu sýfýrla
            else
            {
                currentInput = "";
            }
        }

        // Son inputun üzerinden belirli bir süre geçtiyse, sahneye geçiþ yap
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
                Debug.LogError("Hatalý sahne index'i!, ayný sahne baþlatýlýyor...");
            }
        }
        else
        {
            Debug.LogError("Geçersiz input! Sadece rakamlarý girin.");
        }

        // Sahneye geçtikten sonra inputu sýfýrla
        currentInput = "";

        SceneRestart();
    }

    public void SceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
