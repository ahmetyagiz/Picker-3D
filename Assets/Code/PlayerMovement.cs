using DG.Tweening;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Speed")]
    [SerializeField] private float sideSpeed;
    [SerializeField] private float forwardSpeed;

    [Header("Lose Panel")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private UnityEvent winPanel;

    [Header("Others")]
    [SerializeField] private GameObject pusherCube;
    public bool inCollectCheck;

    private CameraFollow cameraFollow;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (inCollectCheck == true)
        {
            rb.velocity = new Vector3(0, 0, 0);
            return;
        }

        if (Input.touchCount > 0 && inCollectCheck == false)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 localVelocity = new Vector3((touch.deltaPosition.x - Camera.main.ScreenToViewportPoint(Input.mousePosition).x) * sideSpeed, 0, forwardSpeed);
                rb.velocity = transform.TransformDirection(localVelocity);
            }
            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Stationary)
            {
                Vector3 localVelocity = new Vector3(0, 0, forwardSpeed);
                rb.velocity = transform.TransformDirection(localVelocity);
            }
        }
        else if (inCollectCheck == false)
        {
            Vector3 localVelocity = new Vector3(0, 0, forwardSpeed);
            rb.velocity = transform.TransformDirection(localVelocity);
        }
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
        losePanel.SetActive(true);
    }
}