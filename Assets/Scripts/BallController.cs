using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    bool collidingWithItem;
    bool collidingWithBeetle;
    bool instructionalSpace = false;

    public Rigidbody2D rb;
    BeetleController beetle;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        beetle = FindObjectOfType<BeetleController>();
    }

    void FixedUpdate() {
        rb.constraints = RigidbodyConstraints2D.None;

        if (collidingWithItem && collidingWithBeetle) {
            if (!instructionalSpace)
            {
                FindObjectOfType<LevelManager>().ShowInstructionalSpace();
                instructionalSpace = true;
            }
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (beetle.switchingDirectionState == BeetleController.SwitchingDirectionState.Start) {
            // Stop the ball during switching animation
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable")) {
            collidingWithItem = true;
        }

        if (col.CompareTag("Player")) {
            collidingWithBeetle = true;
            var beetleCtrl = col.GetComponent<BeetleController>();
            beetleCtrl.touchingBall = true;
        }

    }

    public void OnCollisionExit2D(Collision2D collision) {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable")) {
            collidingWithItem = false;
        }

        if (col.CompareTag("Player")) {
            collidingWithBeetle = false;
            var beetleCtrl = col.GetComponent<BeetleController>();
            beetleCtrl.touchingBall = false;
        }
    }
}
