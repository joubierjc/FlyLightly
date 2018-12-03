using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour {

	public float karmaGains = 500f;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			GameManager.Instance.karma += karmaGains;
			GameManager.Instance.EndGame(true);
		}
	}

}
