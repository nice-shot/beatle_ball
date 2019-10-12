using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour
{
    public float speed;
    public float groundPressure;
    public LayerMask groundLayer;
    public Transform groundCheck;
    // This gets changed in the ball OnCollision function
    public bool touchingBall;


    Rigidbody2D rb;
    SpriteRenderer sprite;
    Ball ball;
    float horizontalInput;
    bool isGrounded;
    bool shouldSwitchDirection;

    void Awake() {
        touchingBall = false;
        shouldSwitchDirection = false;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        ball = FindObjectOfType<Ball>();
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        sprite.flipX = Mathf.Sign(horizontalInput) == -1;


        shouldSwitchDirection = Input.GetButtonDown("SwitchDirection") && touchingBall;
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
        rb.constraints = RigidbodyConstraints2D.None;

        if (isGrounded) {
            // Prevent sliding down slopes when not moving
            if (Mathf.Approximately(horizontalInput, 0)) {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            } else {
                // Get movement direction based on where we're facing
                Vector2 direction = transform.right * horizontalInput;
                // Apply more pressure to stick to the ground
                direction += (-(Vector2)transform.up * groundPressure);
                // Set velocity
                rb.velocity = direction * speed * Time.fixedDeltaTime;
            }
        }
    }
}
