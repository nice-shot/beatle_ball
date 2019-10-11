using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
	SpriteRenderer spriteRenderer;

	void Start () {
		player = GetComponent<Player> ();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		spriteRenderer.flipX = Mathf.Sign(directionalInput.x) == -1;
	}
}
