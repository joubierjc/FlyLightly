using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

	public float maxValue;
	public float value;
	public float localDecayMultiplicator;
	public UnityEvent onDeath;

	private void Update() {
		value -= Time.deltaTime * localDecayMultiplicator * GameManager.Instance.decayingHealthMultiplicator;
		if (value < 0) {
			onDeath.Invoke();
		}
	}

	public void ToFullLife() {
		value = maxValue;
	}

}
