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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable"))
        {
            canPush = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable"))
        {
            canPush = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
