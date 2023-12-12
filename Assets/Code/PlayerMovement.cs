using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float forwardSpeed;
    public bool inCollectCheck;
    [SerializeField] private GameObject pusherCube;

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
            else if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Stationary )
            {
                rb.velocity = new Vector3(0, 0, forwardSpeed);
            }
        }
        else if(inCollectCheck == false)
        {
            rb.velocity = new Vector3(0, 0, forwardSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectCheck"))
        {
            inCollectCheck = true;
            rb.velocity = Vector3.zero;
            pusherCube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-4.5f);
            pusherCube.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 15);
        }
    }
}