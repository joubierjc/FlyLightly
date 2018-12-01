using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;
	private float score;
	public int multiplicator;

	public float spawnTimeOther { get; set; }

	public float DecayingHealthMultiplicator { get; set; }

	[SerializeField] Text scoreText;

	void Awake()
	{
		if (Instance == null) {
			Instance = this;
		}
		DecayingHealthMultiplicator = 1f;

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
