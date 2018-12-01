using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

	public float maxValue;
	float value;
	float randomDecayFactor;
	public UnityEvent onDeath;

	private void Start()
	{
		ToFullLife();
		randomDecayFactor = Random.Range(1, 3);
	}

	private void Update() {
		value -= Time.deltaTime * randomDecayFactor * GameManager.Instance.DecayingHealthMultiplicator;
		if (value < 0) {
			onDeath.Invoke();
		}
	}

	public void ToFullLife() {
		value = maxValue;
	}

}
