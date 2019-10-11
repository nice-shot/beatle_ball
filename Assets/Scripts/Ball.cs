using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool canPush = true;

    public bool CanPush
    {
        get { return canPush; }
    }

    bool collidingWithItem;
    bool collidingWithBeetle;

    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rb.constraints = RigidbodyConstraints2D.None;

        if (collidingWithItem && collidingWithBeetle) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable")) {
            canPush = false;
            collidingWithItem = true;
        }

        if (col.CompareTag("Player")) {
            collidingWithBeetle = true;
        }

    }

    public void OnCollisionExit2D(Collision2D collision) {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable")) {
            canPush = true;
            collidingWithItem = false;
        }

        if (col.CompareTag("Player")) {
            collidingWithBeetle = false;
        }
    }
}
