using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour
{
    public float speed;
    public float groundPressure;
    public float characterSize;
    public float lastTimeOnGround;
    public GameObject beetle;
    public LayerMask groundLayer;
    public Transform groundCheck;
    // This gets changed in the ball OnCollision function
    private bool _touchingBall;
    public bool touchingBall {
        get { return _touchingBall; }
        set {
            _touchingBall = value;
            if (value == false) {
                ac.SetBool("TouchingBall", false);
                return;
            }

            // If wer'e facing the ball mark as touching it in animation
            bool ballOnRight = transform.position.x < ball.transform.position.x;
            bool facingRight = !sprite.flipX;
            if (ballOnRight == facingRight) {
                ac.SetBool("TouchingBall", true);
            }
        }
    }
    public enum SwitchingDirectionState {Start, Middle, Stop};
    public SwitchingDirectionState switchingDirectionState;

    Rigidbody2D rb;
    Animator ac;
    SpriteRenderer sprite;
    BallController ball;
    float horizontalInput;
    bool isGrounded;
    bool shouldSwitchDirection;
    // Hides the beatle and prevents movements
    bool started;

    void Awake() {
        shouldSwitchDirection = false;
        switchingDirectionState = SwitchingDirectionState.Stop;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        ac = GetComponent<Animator>();
        touchingBall = false;
        Hide();
    }

    public void Hide() {
        started = false;
        sprite.enabled = false;
    }

    public void Show() {
        started = true;
        sprite.enabled = true;
    }

    void Start() {
        ball = FindObjectOfType<BallController>();
    }

    void Update() {
        // Wait until the beetle is shown
        if (!started) {
            return;
        }
        // Not moving when switching directions
        if (switchingDirectionState != SwitchingDirectionState.Stop) {
            horizontalInput = 0;
            shouldSwitchDirection = false;
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (!Mathf.Approximately(horizontalInput, 0)) {
            sprite.flipX = Mathf.Sign(horizontalInput) == -1;
        }

        shouldSwitchDirection = Input.GetButtonDown("SwitchDirection") && touchingBall;
    }

    void StartSwitchingDirection() {
        ac.SetTrigger("SwitchBallSide");
        switchingDirectionState = SwitchingDirectionState.Start;
    }

    void MiddleSwitchingDirection() {
        if (!started) {
            return;
        }
        switchingDirectionState = SwitchingDirectionState.Middle;
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

    void StopSwitchingDirection() {
        switchingDirectionState = SwitchingDirectionState.Stop;
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
        rb.constraints = RigidbodyConstraints2D.None;

        if (isGrounded) {
            lastTimeOnGround = Time.time;
            if (shouldSwitchDirection) {
                StartSwitchingDirection();
            }

            // Prevent sliding down slopes when not moving
            if (Mathf.Approximately(horizontalInput, 0)) {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                ac.SetBool("Walking", false);
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
                ac.SetBool("Walking", true);
            }
        }

        if (!isGrounded)
        {
            float timeUngrounded = Time.time - lastTimeOnGround;
            if (timeUngrounded > 0.5)
            {
                transform.rotation = Quaternion.identity;
                transform.position += Vector3.up * 0.2f;
                lastTimeOnGround = Time.time;
            }
        }
    }
}
