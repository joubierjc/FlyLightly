using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;
	private float score;
	public int multiplicator;

	public float decayingHealthMultiplicator = 1f;

	[SerializeField] Text scoreText;

	void Awake()
	{
		if (Instance == null) {
			Instance = this;
		}
		//else if (Instance != this)
		//	Destroy(gameObject);
	}

	private void Start()
	{
		score = 0;
	}

	private void Update()
	{
		score += Time.deltaTime * multiplicator;
		scoreText.text = ((int) score).ToString();
	}

}
