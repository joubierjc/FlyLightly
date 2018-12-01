using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public static PlayerController Instance = null;

	public ResourceType currentResource = ResourceType.None;

	public float startingInterractCoolDown = 1f;
	public float InterractCoolDown { get; set; }
	public float startingMoveSpeed = 10f;
	public float MoveSpeed { get; set; }
	public Transform groundChecker;

	private bool grounded;
	private Vector2 direction;
	private Rigidbody2D rb2D;

	public LayerMask groundLayer;

	const float groundedRadius = 0.2f;
	const float interractRadius = 0.5f;

	private float nextInterract;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		}

		rb2D = GetComponent<Rigidbody2D>();
		InterractCoolDown = startingInterractCoolDown;
		MoveSpeed = startingMoveSpeed;

		nextInterract = Time.time + InterractCoolDown;
	}

	private void Update() {
		if (nextInterract < Time.time && Input.GetButton("Fire1")) {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interractRadius);
			for (int i = 0; i < colliders.Length; i++) {
				var interractable = colliders[i].GetComponent<Interractable>();
				if (interractable != null) {
					interractable.Interract();
					nextInterract = Time.time + InterractCoolDown;
				}
			}
		}
	}

	private void FixedUpdate() {
		// GET INPUTS
		var horizontal = Input.GetAxisRaw("Horizontal");
		var vertical = grounded && Input.GetButton("Jump") ? 5f : rb2D.velocity.y;


		// GROUND CHECK
		bool wasGrounded = grounded;
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChecker.position, groundedRadius, groundLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject) {
				grounded = true;
				//if (!wasGrounded) {
					//todo
				//}
			}
		}

		// APPLY MOVEMENT
		rb2D.velocity = new Vector2(
			Mathf.Lerp(rb2D.velocity.x, horizontal * MoveSpeed, 0.5f),
			vertical
		);
	}

}
