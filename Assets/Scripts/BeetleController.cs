using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour
{
    public float speed;
    public float groundPressure;
    public float characterSize;
    public LayerMask groundLayer;
    public Transform groundCheck;
    // This gets changed in the ball OnCollision function
    public bool touchingBall;


    Rigidbody2D rb;
    SpriteRenderer sprite;
    BallController ball;
    float horizontalInput;
    bool isGrounded;
    bool shouldSwitchDirection;

    void Awake() {
        touchingBall = false;
        shouldSwitchDirection = false;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Start() {
        ball = FindObjectOfType<BallController>();
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (!Mathf.Approximately(horizontalInput, 0)) {
            sprite.flipX = Mathf.Sign(horizontalInput) == -1;
        }


        shouldSwitchDirection = Input.GetButtonDown("SwitchDirection") && touchingBall;
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
        rb.constraints = RigidbodyConstraints2D.None;

        if (isGrounded) {
            if (shouldSwitchDirection) {
                Vector2 reflection = Vector2.Reflect(ball.transform.position - transform.position, transform.up);
                Vector2 targetPosition = (Vector2)ball.transform.position + reflection;

                RaycastHit2D hitBelow = Physics2D.Raycast(targetPosition, -transform.up, Mathf.Infinity, groundLayer);
                if (hitBelow) {
                    rb.position = targetPosition;
                    sprite.flipX = !sprite.flipX;
                } else {
                    RaycastHit2D hitAbove = Physics2D.Raycast(targetPosition, transform.up, Mathf.Infinity, groundLayer);
                    if (!hitAbove) {
                        Debug.LogError("Can't teleport - not hitting anywhere!");
                    } else {
                        targetPosition = hitAbove.point + hitAbove.normal * (-characterSize / 2);
                        rb.position = targetPosition;
                        sprite.flipX = !sprite.flipX;
                    }
                }

                return;
            }

            // Prevent sliding down slopes when not moving
            if (Mathf.Approximately(horizontalInput, 0)) {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                // Prevent kicking ball
                if (touchingBall && transform.position.y <= ball.transform.position.y) {
                    // TOOD: Make the ball stop
                }
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
