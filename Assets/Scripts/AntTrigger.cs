using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntTrigger : MonoBehaviour
{
    public HoleController hole;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            print("player collided");
            // Find player direction and play ant animation
            bool isOnRight = transform.position.x < col.transform.position.x;
            hole.ShowAnt(isOnRight);
        }
    }
}
