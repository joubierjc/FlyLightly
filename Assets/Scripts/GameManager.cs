using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;
	[HideInInspector]
	public float score;
	public int multiplicator = 20;

	public float startingSpawnTimeOther = 5f;
	public float SpawnTimeOther { get; set; }

	public float startingDecayingHealthMultiplicator = 1f;
	public float DecayingHealthMultiplicator { get; set; }

	public int othersCount = 0;


	public float startingShipHeight = 3000f;
	public float shipHeight;
	public float startingDecreaseByOthers = 5f;
	public float DecreasingHeightByOthers { get; set; }
	public float startingRegen = 15f;
	public float Regen { get; set; }

	[SerializeField] Text scoreText;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		DecayingHealthMultiplicator = startingDecayingHealthMultiplicator;
		SpawnTimeOther = startingSpawnTimeOther;
		DecreasingHeightByOthers = startingDecreaseByOthers;
		shipHeight = startingShipHeight;
		Regen = startingRegen;
}

	private void Start() {
		score = 0;
	}

	private void Update() {
		// SCORE
		score += Time.deltaTime * multiplicator;
		scoreText.text = ((int)score).ToString();

		// SHIP HEIGHT
		shipHeight += (Regen - othersCount * DecreasingHeightByOthers) * Time.deltaTime;
		if (shipHeight < 0) {
			// todo defaite
		}
	}

}
