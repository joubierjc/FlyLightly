using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour {

	public float karmaGains = 500f;

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag == "Player") {
			GameManager.Instance.audioManager.Play("suicide");
			GameManager.Instance.karma += karmaGains;
			GameManager.Instance.EndGame(true);
		}
	}

}
