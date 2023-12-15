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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectCheck"))
        {
            StartCoroutine(nameof(LoseRoutine));

            pusherCube.SetActive(true);
            inCollectCheck = true;
            rb.velocity = Vector3.zero;
            pusherCube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4.5f);
            pusherCube.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 15);

            Invoke(nameof(ResetPusher), 1.5f);
        }

        if (other.gameObject.CompareTag("Dropper"))
        {
            other.GetComponent<BoxCollider>().enabled = false;

            DropperManager currentDropper = other.transform.parent.GetComponent<DropperManager>();
            currentDropper.StartCoroutine(currentDropper.DropObjectRoutine());
        }

        if (other.gameObject.CompareTag("LevelComplete"))
        {
            winPanel.Invoke();
            inCollectCheck = true;
        }

        if (other.gameObject.CompareTag("BigObject"))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.SetActive(true);
        }
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