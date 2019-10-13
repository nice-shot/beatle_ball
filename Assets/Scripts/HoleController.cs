using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public Collector collector;
    public BeetleController beetle;

    public Collider2D ballGrabCollider;
    public Collider2D ballSpriteLayerTriggerCollider;

    private bool antInHole;
    private Animator ac;

    void Awake() {
        ac = GetComponent<Animator>();
        ballGrabCollider.enabled = false;
        ballSpriteLayerTriggerCollider.enabled = false;
    }

    void Update()
    {
        if (collector && collector.Size == 8) {

            ballGrabCollider.enabled = true;
            ballSpriteLayerTriggerCollider.enabled = true;
        }
    }

    void ShowRealBeetle() {
        beetle.Show();
        // ac.SetTrigger("ShowAnt");
    }

    void SetAntInHole() {
        antInHole = true;
    }

    public void ThrowBeetle() {
        ac.SetTrigger("ThrowBeetle");
    }

    public void ShowAnt(bool onRight) {
        ac.SetBool("BeetleOnRight", onRight);
        if (antInHole) {
            ac.SetTrigger("ShowAnt");
            antInHole = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col == col.CompareTag("Ball")) {
            print("Ball entered collision...");
            var sprite = col.GetComponentInChildren<SpriteRenderer>();
            sprite.sortingOrder = 1;
        }
    }
}
