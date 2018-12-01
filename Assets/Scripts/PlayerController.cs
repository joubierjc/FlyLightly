using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public float moveSpeed;

	private bool grounded;
	private Vector2 direction;
	private Rigidbody2D rb2D;

	const float groundedRadius = 0.2f;

	private void Awake() {
		rb2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		// GET INPUTS
		var horizontal = Input.GetAxisRaw("Horizontal");
		var vertical = grounded && Input.GetButton("Jump") ? 5f : rb2D.velocity.y;


		// GROUND CHECK
		// TODO

		// APPLY MOVEMENT
		rb2D.velocity = new Vector2(
			Mathf.Lerp(rb2D.velocity.x, horizontal * moveSpeed, 0.5f),
			vertical
		);
	}



}
