using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float groundDrag = 7f;
    public float jumpForce  = 12f;
    public float jumpCD  = .25f;
    public float airMult = 1.0f;
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode shootKey = KeyCode.Mouse0;

    [Header("GroundCheck")]
    public float playerHeight = 2;
    public LayerMask WhatIsGround;
    bool grounded;

    public Transform orientation;
    public Camera PlayerCam;
    float horizontalInput;
    float verticalInput;
   
    public Vector3 moveDirection;
    private Rigidbody rb;
    public GameObject projectile1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, WhatIsGround);
        //Debug.Log(grounded);
        MyInput();
        SpeedControl();
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            //readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCD);
        }
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMult, ForceMode.Force);

    } 

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Shoot()
    {
        //Find the exact hit position using a raycast
        Ray ray = PlayerCam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else 
            targetPoint = ray.GetPoint(75); //just a point far away from player
        //targetPoint - attackPoint
        Vector3 directionWOSp = targetPoint - transform.position;
        Rigidbody rb = Instantiate(projectile1, orientation.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(directionWOSp.normalized * 32f, ForceMode.Impulse);
    }
}
