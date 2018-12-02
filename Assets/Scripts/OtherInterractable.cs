using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherInterractable : Interractable {
	public float impulseForce = 5f;
	public float scorePenalty = 100f; // ZIZOU

	private void OnEnable() {
		GameManager.Instance.othersCount++;
	}

	public override void Interract() {
		GetComponent<Collider2D>().enabled = false;
		GameManager.Instance.othersCount--;
		GameManager.Instance.score -= scorePenalty;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, impulseForce), ForceMode2D.Impulse);
		Destroy(gameObject, 3f);
	}
}
