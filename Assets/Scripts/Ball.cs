using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] float size = 1;
    private bool canPush = true;
    private Rigidbody2D rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }
    public float Size
    {
        get { return size; }
    }
    public bool CanPush
    {
        get { return canPush; }
    }

    public void Move(Vector2 directionalInput)
    {
        if (canPush)
        {
            if (directionalInput.x == 0)
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.AddForce(directionalInput * size * size, ForceMode2D.Impulse);
            }
        }
    }

    public void Grow(float amount)
    {
        size += 0.5f;
        transform.localScale = new Vector2(size, size);
        // transform.GetChild(0).GetComponent<PushCollider>().Grow(amount);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable"))
        {
            if (!GetComponent<Collector>().CanCollect(col))
            canPush = false;
            rb.velocity = Vector2.zero;
        }

    }

    public void OnCollisionExit2D(Collision2D collision) {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable")) {
            canPush = true;
        }
    }
}
