using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float maxValue;
	public float value;
	[Range(0f, 1f)]
	public float restorePercent = 0.75f;
	[HideInInspector]
	public static float decayFactor;
	public UnityEvent onDeath;

	private bool isDead = false;

	[Header("UI")]
	public Image healthHUD;

	private void Start()
	{
		value = maxValue;
		decayFactor = 1f;
	}

	private void Update() {

		if(isDead)
		{
			return;
		}

		

		value -= Time.deltaTime * decayFactor * GameManager.Instance.DecayingHealthMultiplicator;
		healthHUD.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0f, maxValue, value));

		if (value < 0)
		{

			isDead = true;
			GetComponent<Animator>().SetBool("isDead", isDead);
			var spriteRend = GetComponent<SpriteRenderer>();
			spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, 0.5f);
			if (GetComponent<FriendInterractable>().ressource == ResourceType.Coffee)
			{
				GameManager.Instance.audioManager.Play("death-commander");
			}
			else
			{
				GameManager.Instance.audioManager.Play("death-friend-man");
			}

			onDeath.Invoke();
		}
	}


	public void RestoreHealth() {
		value = Mathf.Lerp(value, maxValue, restorePercent);
	}

}
