using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollower : MonoBehaviour {
    public Rigidbody2D ball;
    public LayerMask collisionMask;
    public float forceAmount;
    public Transform rightTeleport;
    public Transform leftTeleport;

    void Update() {
        transform.position = ball.transform.position;
        // transform.rotation = Quaternion.identity;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, collisionMask);
        if (hit) {
            transform.up = hit.normal;
            ball.AddForce(-transform.up * forceAmount);
        }
    }
}
