using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class CollectManager : MonoBehaviour
{
    [Header("Pusher Cube")]
    [SerializeField] private GameObject pusherCube;

    [Header("Is Player In Collect Check")] 
    public bool inCollectCheck;

    [Header("Win/Lose Panels")]
    [SerializeField] private UnityEvent winPanel;
    [SerializeField] private UnityEvent losePanel;

    private Rigidbody rb;
    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectCheck"))
        {
            StartCoroutine(nameof(LoseRoutine));

            pusherCube.SetActive(true);
            inCollectCheck = true;
            rb.velocity = Vector3.zero;
            pusherCube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4.5f);
            pusherCube.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 15);

            Invoke(nameof(ResetPusher), 1.5f);
        }

        if (other.CompareTag("Dropper"))
        {
            other.GetComponent<BoxCollider>().enabled = false;

            DropperManager currentDropper = other.transform.parent.GetComponent<DropperManager>();
            currentDropper.StartCoroutine(currentDropper.DropObjectRoutine());
        }

        if (other.CompareTag("LevelComplete"))
        {
            winPanel.Invoke();
            inCollectCheck = true;
        }

        if (other.CompareTag("BigObject"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (other.CompareTag("TurnTrigger"))
        {
            CurveRoadType curveRoadType = other.GetComponent<CurveRoadType>();

            if (curveRoadType.turnType == CurveRoadType.TurnType.Left)
            {
                Debug.Log("Sola donuluyor");
                other.GetComponent<BoxCollider>().enabled = false;
                RotateTowards(-transform.right);
                cameraFollow.TurnLeft(other.transform.GetChild(0));
            }
            else if (curveRoadType.turnType == CurveRoadType.TurnType.Right)
            {
                Debug.Log("Saga donuluyor");
                other.GetComponent<BoxCollider>().enabled = false;
                RotateTowards(transform.right);
                cameraFollow.TurnRight(other.transform.GetChild(0));
            }
        }

        if (other.CompareTag("Stickman"))
        {
            other.GetComponent<Animator>().applyRootMotion = false;
        }
    }

    private void RotateTowards(Vector3 direction)
    {
        transform.DORotateQuaternion(Quaternion.LookRotation(direction.normalized).normalized, 1.5f);
    }
    void ResetPusher()
    {
        pusherCube.SetActive(false);
    }

    public IEnumerator LoseRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        losePanel.Invoke();
    }
}
