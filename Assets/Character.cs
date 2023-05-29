using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    
    public int maxJumps = 2;
    
    public float gravity;
    public float dashForce = 10f;
    public float dashDuration = 0.5f;
    private bool isDashing;
    private int jumpsLeft;
    
    private Vector3 moveDirection;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        jumpsLeft = maxJumps;
    }

    private void Update()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveInputX, 0f, moveInputZ).normalized;

        moveDirection *= moveSpeed;

        if (controller.isGrounded)
        {
            jumpsLeft = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash();
        }

        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        
    }
    
    private void Jump()
    {
        if (jumpsLeft > 0)
        {
           moveDirection.y = Mathf.Sqrt(jumpForce * -3.0f * gravity);

            jumpsLeft--;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            StartCoroutine(DashCoroutine());
        }
    }
    
    private IEnumerator DashCoroutine()
    {
        isDashing = true;

        Vector3 dashDirection = moveDirection.normalized;
        float dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            dashTimer += Time.deltaTime;
            controller.Move(dashDirection * dashForce * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }
}