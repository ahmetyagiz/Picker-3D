using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float sideSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float horizontal;
    [SerializeField] private Joystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(horizontal * sideSpeed, 0, forwardSpeed);
    }
}