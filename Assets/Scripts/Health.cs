using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxValue;
	public float value;
	public float localDecayMultiplicator;

	private void Update() {
		value -= Time.deltaTime * localDecayMultiplicator * GameManager.Instance.decayingHealthMultiplicator;
		if (value < 0) {
			// todo mort de ce perso
		}
	}

	public void ToFullLife() {
		value = maxValue;
	}

}
