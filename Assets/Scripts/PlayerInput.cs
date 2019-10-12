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
    private Vector3 offset;
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
        if (Input.GetButtonDown("SwitchDirection") && touchingBall)
        {
            if (controller.collisions.faceDir == 1)
            {
                FindObjectOfType<FocusCamera>().AddToOffset(transform.position - ballFollower.rightTeleport.position);
                transform.position = ballFollower.rightTeleport.position;
            }
            else
            {
                FindObjectOfType<FocusCamera>().AddToOffset(transform.position - ballFollower.leftTeleport.position);
                transform.position = ballFollower.leftTeleport.position;
            }
            touchingBallFromRight = !touchingBallFromRight;
            touchingBallFromLeft = !touchingBallFromLeft;
            offset = ball.transform.position - transform.position;
            ballFollower.forceAmount = ballFollowerForceAmount;
        }
        else
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (touchingBall)
            {
                if (touchingBallFromRight && directionalInput.x <= 0)
                {
                    print("moving left");
                    ball.Move(directionalInput);
                    directionalInput = Vector2.zero;
                    transform.position = ball.transform.position - offset;
                }
                if (touchingBallFromLeft && directionalInput.x >= 0)
                {
                    ball.Move(directionalInput);
                    directionalInput = Vector2.zero;
                    transform.position = ball.transform.position - offset;
                }
            }
            player.SetDirectionalInput(directionalInput);
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            touchingBall = true;
            // ball = collider.GetComponent<PushCollider>().GetBall;
            Vector3 center = collider.bounds.center;
            touchingBallFromRight = transform.position.x > center.x;
            touchingBallFromLeft = transform.position.x < center.x;
            offset = ball.transform.position - transform.position;
            ballFollower.forceAmount = ballFollowerForceAmount;

        }
    }


    void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("Ball")) {
            print("no ball!");
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
