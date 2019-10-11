﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollower : MonoBehaviour {
    public GameObject ball;
    public LayerMask collisionMask;

    void Update() {
        transform.position = ball.transform.position;
        // transform.rotation = Quaternion.identity;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, collisionMask);
        if (hit) {
            print("Hit: " + hit.normal.ToString());
            transform.up = hit.normal;
        }
    }
}