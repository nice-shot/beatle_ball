using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private int curSize = 0;

    public int Size
    {
        get { return curSize; }
    }
        
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.CompareTag("Collectable"))
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
        curSize += 1;
        GetComponent<Animator>().SetTrigger("changeSize");
        //GetComponent<Rigidbody2D>().mass *= 1.5f;
    }

    public bool CanCollect(Collider2D col)
    {
        return curSize >= col.GetComponent<Collectable>().Size;
    }
}
