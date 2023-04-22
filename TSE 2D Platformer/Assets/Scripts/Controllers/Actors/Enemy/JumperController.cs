using Actors.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpInterval = 2f;
    public LayerMask blockLayer;
    public Transform groundCheck;
    public bool grounded;
    public GameObject player;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private float jumpTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isJumping)
        {
            // If the jump timer has expired, jump in a random direction
            if (jumpTimer <= 0f)
            {
                float randomDirection = Random.Range(-3f, 3f);
                rb.velocity = new Vector2(randomDirection, jumpForce);
                isJumping = true;
                jumpTimer = jumpInterval;
            }
            else
            {
                jumpTimer -= Time.deltaTime;
            }
        }

        if (onGround())
        {
            grounded = true;
            isJumping = false;
        }
        else grounded = false;
    }

    private bool onGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, blockLayer);
    }
}
