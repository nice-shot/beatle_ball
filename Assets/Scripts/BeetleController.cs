using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 velocity = transform.right * Input.GetAxis("Horizontal");
        rb.velocity = velocity * speed * Time.deltaTime;
    }
}
