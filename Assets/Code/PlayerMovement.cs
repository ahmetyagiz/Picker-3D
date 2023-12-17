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

    private CollectManager collectManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collectManager = GetComponent<CollectManager>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (collectManager.inCollectCheck == true)
        {
            rb.velocity = new Vector3(0, 0, 0);
            return;
        }

        if (Input.touchCount > 0 && collectManager.inCollectCheck == false)
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
        else if (collectManager.inCollectCheck == false)
        {
            Vector3 localVelocity = new Vector3(0, 0, forwardSpeed);
            rb.velocity = transform.TransformDirection(localVelocity);
        }
    }
}