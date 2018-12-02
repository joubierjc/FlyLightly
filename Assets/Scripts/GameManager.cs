using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;
	[HideInInspector]
	public float score;
	public int multiplicator = 20;

	public OthersSpawn othersSpawner;
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

	private bool gameIsPaused = false;

	[SerializeField] Text scoreText;
	[SerializeField] Text altitudeText;

	public GameObject startMenu;
	public GameObject pauseMenu;

	public AudioManager audioManager;	

	void Awake()
	{
		Time.timeScale = 0f;
		if (Instance == null) {
			Instance = this;
		}
		audioManager.Play("menuStart");
		Init();
	}

	private void Init()
	{
		if (Time.timeScale == 1f)
		{
			DecayingHealthMultiplicator = startingDecayingHealthMultiplicator;
			SpawnTimeOther = startingSpawnTimeOther;
			DecreasingHeightByOthers = startingDecreaseByOthers;
			shipHeight = startingShipHeight;
			Regen = startingRegen;

			score = 0;
		}
	}

	public void NewGame()
	{
		Time.timeScale = 1f;
		audioManager.Stop("menuStart");
		audioManager.Play("theme");
		startMenu.SetActive(false);
		Init();
		othersSpawner.StartSpawn();
	}

	public void MenuButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ResumeGame()
	{
		gameIsPaused = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	private void PauseGame()
	{
		gameIsPaused = true;
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
	}

	public void QuitGame()
	{
		Application.Quit();
	}



	private void Update() {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameIsPaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}

		// SCORE
		score += Time.deltaTime * multiplicator;
		scoreText.text = ((int)score).ToString();

		// SHIP HEIGHT
		shipHeight += (Regen - othersCount * DecreasingHeightByOthers) * Time.deltaTime;
		altitudeText.text = ((int)shipHeight).ToString();
		if (shipHeight < 0) {
			// todo defaite
		}
	}

}
