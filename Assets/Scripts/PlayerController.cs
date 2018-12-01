﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public Transform groundChecker;

	private bool grounded;
	private Vector2 direction;
	private Rigidbody2D rb2D;

	public LayerMask groundLayer;
	public float radius;

	const float groundedRadius = 0.2f;

	private void Awake() {
		rb2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		// GET INPUTS
		var horizontal = Input.GetAxisRaw("Horizontal");
		var vertical = grounded && Input.GetButton("Jump") ? 5f : rb2D.velocity.y;


		// GROUND CHECK
		bool wasGrounded = grounded;
		grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, radius, groundLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
				//if (!wasGrounded)
					//todo
			}
		}

		// APPLY MOVEMENT
		rb2D.velocity = new Vector2(
			Mathf.Lerp(rb2D.velocity.x, horizontal * moveSpeed, 0.5f),
			vertical
		);
	}



}
