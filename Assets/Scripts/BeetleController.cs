using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour
{
    public float speed;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public bool isGrounded;

    Rigidbody2D rb;


    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);

        if (isGrounded) {
            Vector2 direction = transform.right * Input.GetAxisRaw("Horizontal");
            print("Changing velocity to: " + direction.ToString());
            rb.velocity = direction * speed * Time.deltaTime;
        }

    }
}
