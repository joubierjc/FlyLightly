using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float maxValue;
	public float value;
	float randomDecayFactor;
	public UnityEvent onDeath;

	[Header("UI")]
	public Image healthHUD;

	private void Start()
	{
		ToFullLife();
		randomDecayFactor = Random.Range(1, 3);
	}

	private void Update() {
		value -= Time.deltaTime * randomDecayFactor * GameManager.Instance.DecayingHealthMultiplicator;
		healthHUD.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0f, maxValue, value));
		if (value < 0) {
			onDeath.Invoke();
		}
	}

	public void ToFullLife() {
		value = maxValue;
	}

}
