using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    bool collidingWithItem;
    bool collidingWithBeetle;

    public Rigidbody2D rb;

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
