using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

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
    private Color transitionColor;
    private PlayerMovement playerMovement;
    [SerializeField] private bool isEndingChecker;
    [SerializeField] private Color progressBarColor;
    [SerializeField] private UnityEvent progressBarActivation;
    public List<GameObject> collectedObjects;

    private void Start()
    {
        counterText.text = collectIndex + " / " + collectGoal;
        playerMovement = FindObjectOfType<PlayerMovement>();
        transitionColor = GameObject.FindGameObjectWithTag("Ground").GetComponent<MeshRenderer>().material.color;
    }

    bool isCheckerCompleted;

    void CollectedAnObject(GameObject other, int score)
    {
        collectIndex += score;
        counterText.text = collectIndex + " / " + collectGoal;
        collectedObjects.Add(other);
        other.GetComponent<MeshRenderer>().material.color = transitionColor;

        if (collectIndex >= collectGoal && !isCheckerCompleted)
        {
            playerMovement.StopAllCoroutines();

            StartCoroutine(nameof(CompleteRoutine));

            isCheckerCompleted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableObject"))
        {
            CollectedAnObject(other.gameObject, 1);
        }

        if (other.gameObject.CompareTag("Stickman"))
        {
            CollectedAnObject(other.gameObject, 3);
        }
    }

    IEnumerator CompleteRoutine()
    {
        yield return new WaitForSeconds(1f);

        foreach (GameObject obj in collectedObjects)
        {
            obj.SetActive(false);
        }

        bridgePlatform.GetComponent<MeshRenderer>().material.color = transitionColor;

        yield return new WaitForSeconds(0.1f);

        bridgePlatform.transform.DOMoveY(-0.87f, 0.5f).SetEase(Ease.OutBack, 3f).OnComplete(() =>
        {
            _gateAnimator.SetTrigger("Open");
        });

        yield return new WaitForSeconds(1);

        playerMovement.inCollectCheck = false;
        progressBarActivation.Invoke();
        stopper.SetActive(false);
    }
}