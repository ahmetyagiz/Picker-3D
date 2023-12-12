using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class CollectChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int collectIndex;
    [SerializeField] private int collectGoal;
    [SerializeField] private GameObject bridgePlatform;
    [SerializeField] private GameObject stopper;
    private bool isLevelCompleted;
    private PlayerMovement playerMovement;
    [SerializeField] private Color color;
    [SerializeField] private Color defaultColor;

    private void Start()
    {
        counterText.text = collectIndex + " / " + collectGoal;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableObject"))
        {
            collectIndex++;
            counterText.text = collectIndex + " / " + collectGoal;

            if (collectIndex == collectGoal && !isLevelCompleted)
            {
                StartCoroutine(nameof(CompleteRoutine));
            }
        }
    }

    IEnumerator CompleteRoutine()
    {
        yield return new WaitForSeconds(1);

        isLevelCompleted = true;
        bridgePlatform.transform.DOMoveY(-0.87f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            bridgePlatform.GetComponent<MeshRenderer>().material.color = color;
        });

        yield return new WaitForSeconds(1);
        stopper.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.inCollectCheck = false;

    }
}