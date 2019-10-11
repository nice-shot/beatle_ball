using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] float size = 1;

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.GetComponent<Collectable>() != null)
        {
            if (CanCollect(col))
            {
                Collect(col);
            }
        }
    }

    private void Collect(Collider2D col)
    {
        Destroy(col.gameObject);
        size += 0.5f;
        transform.localScale = new Vector2(size, size);
    }

    private bool CanCollect(Collider2D col)
    {
        return size >= col.GetComponent<Collectable>().Size;
    }
}
