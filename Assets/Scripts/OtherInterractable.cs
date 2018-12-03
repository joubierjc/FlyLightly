using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OtherInterractable : Interractable {
	public float impulseForce = 5f;
	public float karmaPenalty = 100f; // ZIZOU
	public Vector2 randomRotationValues = new Vector2(-120f, 120f);
	public float deathDuration = 3f;

	private void OnEnable() {
		GameManager.Instance.othersCount++;
	}

	public override void Interract() {
		GetComponent<Collider2D>().enabled = false;
		GameManager.Instance.othersCount--;
		GameManager.Instance.karma -= karmaPenalty;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, impulseForce), ForceMode2D.Impulse);

		transform.DORotate(
			new Vector3(
				0f,
				0f,
				Random.Range(randomRotationValues.x, randomRotationValues.y)
			), deathDuration
		);

		float rd = Random.value;
		if(rd < 0.33f)
		{
			GameManager.Instance.audioManager.Play("death-other-1");
		}
		else if( rd < 0.66f)
		{
			GameManager.Instance.audioManager.Play("death-other-2");
		}
		else
		{
			GameManager.Instance.audioManager.Play("death-other-3");
		}

		Destroy(gameObject, deathDuration);
	}
}
