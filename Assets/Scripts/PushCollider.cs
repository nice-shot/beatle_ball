using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCollider : MonoBehaviour
{
    private Ball ball;
    public float size = 1;

    public Ball GetBall
    {
        get { return ball; }
    }

    public void Start()
    {
        ball = transform.parent.GetComponent<Ball>();
    }

    public void Grow(float amount)
    {
        size += 0.5f;
        transform.localScale = new Vector2(size, size);
    }
}
