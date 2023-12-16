using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelProgressCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;

    private void Start()
    {
        currentLevelText.text = PlayerPrefs.GetInt("FakeLevelIndex").ToString();
        nextLevelText.text = (PlayerPrefs.GetInt("FakeLevelIndex") + 1).ToString();
    }
}
