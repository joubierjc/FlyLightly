using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Events;
using Cinemachine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;
	[HideInInspector]
	public float score;
	[HideInInspector]
	public float karma;
	public float maxDistance = 5000f;
	public int multiplicator = 20;

	private bool isSacrificing = false;

	[Header("Health Decay Settings")]
	public float startingDecayingHealthMultiplicator = 1f;
	public float DecayingHealthMultiplicator { get; set; }
	public float timeBetweenHealthDecayIncrease = 10f;
	public float additionnalDecay = 0.2f;

	[HideInInspector]
	public int othersCount = 0;
	[HideInInspector]
	public int friendCount = 3;

	[Header("Ship Height")]
	public float startingShipHeight = 3000f;
	public float shipHeight;
	public float CurrentGains { get; private set; }

	[Header("Other Settings")]
	public OthersSpawn othersSpawner;
	public float startingSpawnTimeOther = 5f;
	public float SpawnTimeOther { get; set; }
	public float startingDecreaseByOthers = 5f;
	public float DecreasingHeightByOthers { get; set; }
	public UnityEvent onTopDied;

	public float startingRegen = 15f;
	public float Regen { get; set; }

	[Header("Stun Settings")]
	public float startingStunInterval = 20f;
	public float StunInterval { get; set; }
	public GameObject VCFollow;
	public GameObject VCFollowShake;
	public float stunDuration = 1f;

	private bool gameIsPaused = false;
	private bool gameOver = false;

	[SerializeField] Text scoreText;
	[SerializeField] Text altitudeText;

	[Header("Menus")]
	public GameObject hud;
	public GameObject startMenu;
	public GameObject pauseMenu;
	public Image endPanel;
	public GameObject endMenu;
	public GameObject helpMenu;
	public GameObject playHelpMenu;
	public GameObject storyMenu;
	public float transitionDuration = 1f;
	public Ease curve;

	[Header("End Settings")]
	public GameObject congrats;
	public Text finalDistanceText;
	public Text karmaText;
	public Text karmaMotivationalText;

	[Header("Audio Manager")]
	public AudioManager audioManager;

	private float nextStun;
	private float dummyValue;
	private bool doom = false;

	void Awake() {
		//PlayerPrefs.SetInt("help-seen", 0);
		friendCount = 3;
		doom = false;

		Time.timeScale = 0f;
		if (Instance == null) {
			Instance = this;
		}
		PlaySound("menuStart");
		Init();
		nextStun = Time.time + StunInterval;
	}

	private void Init() {
		if (Time.timeScale == 1f) {
			DecayingHealthMultiplicator = startingDecayingHealthMultiplicator;
			SpawnTimeOther = startingSpawnTimeOther;
			DecreasingHeightByOthers = startingDecreaseByOthers;
			shipHeight = startingShipHeight;
			Regen = startingRegen;
			StunInterval = startingStunInterval;

			StartCoroutine(IncreaseDecayFactor());
			score = 0;
		}
	}

	IEnumerator IncreaseDecayFactor() {
		Health.decayFactor = 1f;
		yield return new WaitForSeconds(timeBetweenHealthDecayIncrease);

		while (enabled && Health.decayFactor <= 2.5f) {
			Health.decayFactor += additionnalDecay;
			yield return new WaitForSeconds(timeBetweenHealthDecayIncrease);
		}

		yield break;
	}

	public void DisplayStory() {
		startMenu.SetActive(false);
		storyMenu.SetActive(true);
	}

	public void NewGame() {

		storyMenu.SetActive(false);
		playHelpMenu.SetActive(false);
		helpMenu.SetActive(false);

		var helpSeen = PlayerPrefs.GetInt("help-seen", 0);
		if (helpSeen != 1) {
			HelpButton(false);
			return;
		}

		Time.timeScale = 1f;

		audioManager.Stop("menuStart");
		DOTween.To(
			() => VCFollow.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize,
			x => VCFollow.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = x,
			3f,
			transitionDuration
		).SetEase(curve)
		.OnComplete(() => {
			PlaySound("theme");
			hud.SetActive(true);
		});

		startMenu.SetActive(false);
		Init();
		othersSpawner.StartSpawn();
		nextStun = Time.time + StunInterval;
	}

	public void PlaySound(string name) {
		audioManager.Play(name);
	}

	public void MenuButton() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ResumeGame() {
		gameIsPaused = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
	}

	private void PauseGame() {
		gameIsPaused = true;
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
	}

	public void HelpButton(bool fromMenu = true) {
		startMenu.SetActive(false);

		PlayerPrefs.SetInt("help-seen", 1);
		if (fromMenu) {
			playHelpMenu.SetActive(false);
			helpMenu.SetActive(true);
		}
		else {
			playHelpMenu.SetActive(true);
			helpMenu.SetActive(false);
		}
	}

	public void QuitGame() {
		Application.Quit();
	}

	Tween endTween = null;
	public void EndGame(bool sacrifice = false) {
		gameOver = true;

		if (sacrifice) {
			isSacrificing = true;
		}

		endTween.Kill();
		endTween = DOTween.To(
			() => endPanel.color,
			x => endPanel.color = x,
			new Color(endPanel.color.r, endPanel.color.g, endPanel.color.b, 1f),
			transitionDuration
		).OnComplete(() => {
			audioManager.Stop("theme");
			endMenu.SetActive(true);
			Time.timeScale = 0f;

			karmaText.text = ((int)karma).ToString();
			karmaText.color = karma >= 0f ? Color.green : Color.red;

			finalDistanceText.text = ((int)Mathf.Clamp(score, 0f, maxDistance)).ToString() + "m";
			if (score >= maxDistance) {
				congrats.SetActive(true);
			}

			if (shipHeight < 100f) {
				audioManager.Play("boom");
			}

			if (isSacrificing) {
				GameManager.Instance.audioManager.Play("suicide");
				karmaMotivationalText.text = karma >= 0f ? "You were to good for this world..." : "You chose to sacrifice yourself, but was it worth?";
			}
			else {
				karmaMotivationalText.text = karma >= 0f ? "Choices had to be made, we won't judge you..." : "You've come this far, but at what cost?";
			}

			karmaMotivationalText.text = karmaMotivationalText.text.ToUpper();
		});
	}

	private void Update() {

		// SHIP HEIGHT
		CurrentGains = (Regen - othersCount * DecreasingHeightByOthers);
		shipHeight += CurrentGains * Time.deltaTime;
		altitudeText.text = ((int)shipHeight).ToString();

		if (gameOver) {
			return;
		}

		if (score >= maxDistance) {
			EndGame();
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (gameIsPaused) {
				ResumeGame();
			}
			else {
				PauseGame();
			}
		}

		// STUN SCREENSHAKE
		if (nextStun < Time.time) {
			nextStun = Time.time + StunInterval;

			audioManager.Play("wind");
			PlayerController.Instance.stunned = true;
			VCFollowShake.SetActive(true);
			VCFollow.SetActive(false);

			dummyValue = 0f;
			DOTween.To(() => dummyValue, x => dummyValue = x, 1f, stunDuration).OnComplete(() => {
				VCFollow.SetActive(true);
				VCFollowShake.SetActive(false);
				PlayerController.Instance.stunned = false;
			});
		}

		// SCORE
		score += Time.deltaTime * multiplicator;
		scoreText.text = ((int)score).ToString();

		if (shipHeight < 100f) {
			audioManager.Stop("theme");
			EndGame();
		}

		if (!doom && friendCount < 1) {
			doom = true;
			onTopDied.Invoke();
		}
	}

}
