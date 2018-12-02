using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public static PlayerController Instance = null;

	[Header("Interract settings")]
	public SpriteRenderer interractRenderer;
	public Sprite interractSprite;

	[Header("Resources settings")]
	public ResourceType currentResource = ResourceType.None;
	public SpriteRenderer resourceRenderer;
	public Sprite ammo;
	public Sprite oil;
	public Sprite food;
	public Sprite coffee;

	public float jumpForce = 10f;

	public float startingInterractCoolDown = 1f;
	public float InterractCoolDown { get; set; }
	public float startingMoveSpeed = 10f;
	public float MoveSpeed { get; set; }
	public Transform groundChecker;

	[Header("Animation")]
	public Animator animator;

	private bool grounded;
	private Vector2 direction;
	private Rigidbody2D rb2D;

	public LayerMask groundLayer;

	const float groundedRadius = 0.2f;
	const float interractRadius = 0.5f;

	private float nextInterract;
	private float prevHorizontalDirection = 1f;

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
		switch (currentResource) {
			case ResourceType.None:
				resourceRenderer.sprite = null;
				break;
			case ResourceType.Ammo:
				resourceRenderer.sprite = ammo;
				break;
			case ResourceType.Oil:
				resourceRenderer.sprite = oil;
				break;
			case ResourceType.Food:
				resourceRenderer.sprite = food;
				break;
			case ResourceType.Coffee:
				resourceRenderer.sprite = coffee;
				break;
			default:
				resourceRenderer.sprite = null;
				break;
		}

		var interractables = Physics2D.OverlapCircleAll(transform.position, interractRadius)
			.Select(e => e.GetComponent<Interractable>())
			.Where(e => e != null)
			.ToArray();

		if (interractables.Length > 0) {
			interractRenderer.sprite = interractSprite;
			if (nextInterract < Time.time && Input.GetButton("Fire1")) {
				interractables[0].Interract();
				nextInterract = Time.time + InterractCoolDown;
			}
		}
		else {
			interractRenderer.sprite = null;
		}
	}

	private void FixedUpdate() {
		// GET INPUTS
		var horizontal = Input.GetAxisRaw("Horizontal");
		var vertical = grounded && Input.GetButton("Jump") ? jumpForce : rb2D.velocity.y;

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

		// UPDATE ANIMATOR PARAMS
		prevHorizontalDirection = horizontal > 0f ? -1f : (horizontal < 0f ? 1f : prevHorizontalDirection);
		transform.localScale = new Vector3(prevHorizontalDirection, 1f, 1f);
		animator.SetFloat("MoveSpeed", Mathf.Abs(rb2D.velocity.x));
	}

}
