using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatleController : MonoBehaviour {
    public float speed;
    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float movement = Input.GetAxis("Horizontal");
        if (movement != 0) {
            Vector2 newPosition = transform.position;
            newPosition.x += movement * speed * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
