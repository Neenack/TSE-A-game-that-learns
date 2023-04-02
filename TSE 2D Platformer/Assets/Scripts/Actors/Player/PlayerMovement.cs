using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer, ladderLayer;

    private float horizontal, vertical;
    public float playerSpeed, jumpingPower, climbSpeed, gravity;
    private bool isFacingRight = true;
    private bool isClimbing;

    private float timeBuffer = 0.1f;

    private float edgeTimer, jumpBufferTimer;

    // Update is called once per frame
    void Update()
    {
        //Flips the player in the direction of movement
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) Flip();

        //Toggles isClimbing on and off is the player is colliding with a ladder
        if (isLadder() && Mathf.Abs(vertical) > 0f) isClimbing = true;
        else isClimbing = false;

        //Extra jump time when player goes off an edge
        if (isGrounded())
        {
            edgeTimer = timeBuffer;

            //Player jumps as soon as it touches the ground if jump buffer still active
            if (jumpBufferTimer > 0f) rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else edgeTimer -= Time.deltaTime;

        //Jump buffer timer
        if (jumpBufferTimer > 0f) jumpBufferTimer -= Time.deltaTime;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    //Player jumping
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Create a jump buffer if the player jumps before hitting the ground
            if (!isGrounded()) jumpBufferTimer = timeBuffer;

            //Allow the player to jump if still in edge timer or on a ladder
            if (edgeTimer > 0f || isLadder())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
        }

        //If space is held longer then jump higher
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    void FixedUpdate()
    {
        //Player horizontal movement
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);

        //Handles player ladder climbing
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
        else
        {
            rb.gravityScale = gravity;
        }
    }

    //Checks if player is on ground
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Checks if player is touching a ladder
    private bool isLadder()
    {
        return Physics2D.OverlapCircle(transform.position, 0.2f, ladderLayer);
    }

    //Flips the player
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
