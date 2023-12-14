using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectChecker : MonoBehaviour
{
    [Header("Collect Details")]
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int collectIndex;
    [SerializeField] private int collectGoal;

    [Header("--")]
    [SerializeField] private GameObject bridgePlatform;
    [SerializeField] private GameObject stopper;
    [SerializeField] private Animator _gateAnimator;
    [SerializeField] private Color color;
    private PlayerMovement playerMovement;
    [SerializeField] private bool isEndingChecker;
    [SerializeField] private Image progressImage;
    [SerializeField] private Color progressBarColor;
    
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

            if (collectIndex == collectGoal)
            {
                playerMovement.StopAllCoroutines();

                StartCoroutine(nameof(CompleteRoutine));
            }

            Destroy(other.gameObject, 0.65f);
        }
    }

    IEnumerator CompleteRoutine()
    {
        yield return new WaitForSeconds(1);

        bridgePlatform.transform.DOMoveY(-0.87f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            bridgePlatform.GetComponent<MeshRenderer>().material.color = color;
            _gateAnimator.SetTrigger("Open");
        });

        yield return new WaitForSeconds(1);

        playerMovement.inCollectCheck = false;
        progressImage.color = progressBarColor;
        stopper.SetActive(false);
    }
}