using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;

public class CollectChecker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int collectIndex;
    [SerializeField] private int collectGoal;
    [SerializeField] private GameObject bridgePlatform;
    [SerializeField] private GameObject stopper;
    [SerializeField] private Animator _gateAnimator;
    [SerializeField] private Color color;
    private bool isLevelCompleted;
    private PlayerMovement playerMovement;
    [SerializeField] private bool isEndingChecker;
    [SerializeField] private UnityEvent winEvent;

    private void Start()
    {
        counterText.text = collectIndex + " / " + collectGoal;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableObject"))
        {
            collectIndex++;
            counterText.text = collectIndex + " / " + collectGoal;

            if (collectIndex == collectGoal && !isLevelCompleted)
            {
                playerMovement.StopAllCoroutines();

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
            _gateAnimator.SetTrigger("Open");
        });

        yield return new WaitForSeconds(1);

        if (isEndingChecker)
        {
            winEvent.Invoke();
        }
        else
        {
            playerMovement.inCollectCheck = false;
        }
        stopper.SetActive(false);
    }
}