using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] Collector collector;
    
    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (collector.Size == 8)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            var sprite = col.GetComponentInChildren<SpriteRenderer>();
            sprite.sortingOrder = 1;
        }
    }
}
