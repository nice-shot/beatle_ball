using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
	Controller2D controller;
	SpriteRenderer spriteRenderer;

	bool touchingBall;

	void Awake () {
		touchingBall = false;
		player = GetComponent<Player>();
		controller = GetComponent<Controller2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetButtonDown("SwitchDirection") && touchingBall) {
			print("Switching direction");
		}

		CheckRotation();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Ball")) {
			touchingBall = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Ball")) {
			touchingBall = false;
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
