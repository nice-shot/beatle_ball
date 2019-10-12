using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
        
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
        GetComponent<Ball>().Grow(0.5f);
    }

    public bool CanCollect(Collider2D col)
    {
        return GetComponent<Ball>().Size >= col.GetComponent<Collectable>().Size;
    }
}
