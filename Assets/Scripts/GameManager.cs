using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;
	[HideInInspector]
	public float score;
	[HideInInspector]
	public float karma;
	public float maxDistance = 5000f;
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
	private bool gameOver = false;

	[SerializeField] Text scoreText;
	[SerializeField] Text altitudeText;

	[Header("Menus")]
	public GameObject startMenu;
	public GameObject pauseMenu;
	public GameObject endMenu;
	public GameObject helpMenu;

	[Header("End Settings")]
	public GameObject congrats;
	public Text finalDistanceText;
	public Text karmaText;
	public Text karmaMotivationalText;

	[Header("Audio Manager")]
	public AudioManager audioManager;

	void Awake()
	{
		Time.timeScale = 0f;
		if (Instance == null) {
			Instance = this;
		}
		PlaySound("menuStart");
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
		PlaySound("theme");
		startMenu.SetActive(false);
		Init();
		othersSpawner.StartSpawn();
	}

	public void PlaySound(string name)
	{
		audioManager.Play(name);
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

	public void HelpButton()
	{
		helpMenu.SetActive(true);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void EndGame(bool sacrifice = false) {
		audioManager.Stop("theme");

		gameOver = true;
		endMenu.SetActive(true);
		Time.timeScale = 0f;

		karmaText.text = ((int)karma).ToString();
		karmaText.color = karma >= 0f ? Color.green : Color.red;

		finalDistanceText.text = ((int)score).ToString() + "m";
		if (score >= maxDistance) {
			congrats.SetActive(true);
		}

		if (sacrifice) {
			karmaMotivationalText.text = karma >= 0f ? "You were to good for this world..." : "You chose to sacrifice yourself, but was it worth?";
		}
		else {
			karmaMotivationalText.text = karma >= 0f ? "Choices had to be made, we won't judge you..." : "You've come this far, but at what cost?";
		}

		karmaMotivationalText.text = karmaMotivationalText.text.ToUpper();
	}

	private void Update() {

		if (gameOver) {
			return;
		}

		if (score >= maxDistance) {
			EndGame();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
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
			audioManager.Stop("theme");
			audioManager.Play("boom");
			EndGame();
		}
	}

}
