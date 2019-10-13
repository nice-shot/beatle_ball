using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public Collector collector;
    public BeetleController beetle;

    private Collider2D[] colliders;
    private Animator ac;

    void Awake() {
        ac = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }
    }

    void Update()
    {
        if (collector && collector.Size == 8) {
            foreach (Collider2D collider in colliders) {
                collider.enabled = true;
            }
        }
    }

    void ShowRealBeetle() {
        beetle.Show();
        ac.SetTrigger("ShowAnt");
    }

    public void ThrowBeetle() {
        ac.SetTrigger("ThrowBeetle");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            var sprite = col.GetComponentInChildren<SpriteRenderer>();
            sprite.sortingOrder = 1;
            StartCoroutine("Win");
            
        }
    }

    public IEnumerator Win()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<LevelManager>().Win();
    }
}
