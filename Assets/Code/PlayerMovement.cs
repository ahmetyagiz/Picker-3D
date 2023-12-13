using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Speed")]
    [SerializeField] private float sideSpeed;
    [SerializeField] private float forwardSpeed;

    [Header("Win Lose Panels")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    [Header("Others")]
    [SerializeField] private GameObject pusherCube;
    public bool inCollectCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
                rb.velocity = new Vector3((touch.deltaPosition.x - Camera.main.ScreenToViewportPoint(Input.mousePosition).x) * sideSpeed, 0, forwardSpeed);
            }
            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Stationary)
            {
                rb.velocity = new Vector3(0, 0, forwardSpeed);
            }
        }
        else if (inCollectCheck == false)
        {
            rb.velocity = new Vector3(0, 0, forwardSpeed);
        }
    }

    private bool isLoseRoutineActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectCheck"))
        {
            Debug.Log("Fail Routine baþladý: " + other.gameObject.name);
            StartCoroutine(nameof(LoseRoutine));


            inCollectCheck = true;
            rb.velocity = Vector3.zero;
            pusherCube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4.5f);
            pusherCube.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 15);
        }
    }

    public IEnumerator LoseRoutine()
    {
        yield return new WaitForSeconds(4);
        losePanel.SetActive(true);
    }
}