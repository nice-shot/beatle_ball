using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
	Controller2D controller;
	SpriteRenderer spriteRenderer;
    Ball ball;
    bool touchingBall;
    bool touchingBallFromRight;
    bool touchingBallFromLeft;
	BallFollower ballFollower;
	public float ballFollowerForceAmount;

    void Awake () {
		touchingBall = false;
		player = GetComponent<Player>();
		controller = GetComponent<Controller2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	void Start() {
		ballFollower = FindObjectOfType<BallFollower>();
		ballFollowerForceAmount = ballFollower.forceAmount;
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
        //if (touchingBall)
        //{
        //    print("touching ball");
        //    if (!ball.CanPush)
        //    {
        //        print("can't push ball");
        //        if (touchingBallFromRight)
        //        {
        //            directionalInput = new Vector2(Mathf.Max(directionalInput.x, 0), 0);
        //        }
        //        else if (touchingBallFromLeft)
        //        {
        //            directionalInput = new Vector2(Mathf.Min(directionalInput.x, 0), 0);
        //        }
        //    }
        //}
		player.SetDirectionalInput (directionalInput);

		if (Input.GetButtonDown("SwitchDirection") && touchingBall) {
			if (controller.collisions.faceDir == 1) {
				transform.position = ballFollower.rightTeleport.position;
			} else {
				transform.position = ballFollower.leftTeleport.position;
			}
		}

		CheckRotation();
	}

	void StickToGround() {
		Transform spriteTrans = spriteRenderer.transform;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -spriteTrans.up, Mathf.Infinity, controller.collisionMask);
		if (hit) {
			transform.position = Vector2.MoveTowards(transform.position, hit.transform.position, 10	);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Ball")) {
            Collider2D collider = collision.collider;
			touchingBall = true;
            ball = collider.GetComponent<Ball>();
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;
            touchingBallFromRight = contactPoint.x > center.x;
            touchingBallFromLeft = contactPoint.x < center.x;
			ballFollower.forceAmount = ballFollowerForceAmount;
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Ball")) {
			touchingBall = false;
            touchingBallFromRight = false;
            touchingBallFromLeft = false;
            ball = null;
			ballFollower.forceAmount = 0;
		}
	}

	void CheckRotation() {
		spriteRenderer.flipX = controller.collisions.faceDir == -1;
		Transform spriteTrans = spriteRenderer.transform;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -spriteTrans.up, Mathf.Infinity, controller.collisionMask);
		if (hit) {
			spriteTrans.up = hit.normal;
			// print(hit.normal);
			// print(spriteTrans.up);
			// Quaternion rot = Quaternion.FromToRotation(spriteTrans.up, hit.normal);
			// rot *= Quaternion.Euler(0, spriteTrans.rotation.y, spriteTrans.rotation.z);
			// print("Rotation: " + rot.ToString());
			// spriteTrans.rotation = Quaternion.Lerp(spriteTrans.rotation, rot, Time.deltaTime * spriteRotationSmooth);;
		}
	}
}
