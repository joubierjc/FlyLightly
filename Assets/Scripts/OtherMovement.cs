using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OtherMovement : MonoBehaviour
{

	[SerializeField] Vector2 rangeWaitTime = new Vector2(2f, 3f);
	[SerializeField] float timeToReachPosition = 1f;
	[SerializeField] float stepRange = .5f;
	[SerializeField] float leftLimit;
	[SerializeField] float rightLimit;

	[Header("Animation")]
	public Animator animator;

	private float MoveSpeed = 0f;
	// Use this for initialization
	void Start()
	{
		StartCoroutine(Move());
	}

	IEnumerator Move()
	{
		while (enabled)
		{
			float posX = transform.position.x;
			if (posX + stepRange < rightLimit && posX - stepRange > leftLimit)
			{
				float random = Random.value;
				MoveSpeed = 1f;
				animator.SetFloat("MoveSpeed", MoveSpeed);
				if (random < 0.5f)
				{
					//Move Left
					transform.DOMoveX(posX - stepRange, timeToReachPosition).OnComplete(() =>
					{
						MoveSpeed = 0f;
						animator.SetFloat("MoveSpeed", MoveSpeed);
					});

					transform.localScale = new Vector3(1f, 1f, 1f);
				}
				else
				{
					//Move Right
					transform.DOMoveX(posX + stepRange, timeToReachPosition).OnComplete(() =>
					{
						MoveSpeed = 0f;
						animator.SetFloat("MoveSpeed", MoveSpeed);
					});

					transform.localScale = new Vector3(-1f, 1f, 1f);
				}
			}
			else if (posX + stepRange > rightLimit)
			{
				//Move Left
				MoveSpeed = 1f;
				animator.SetFloat("MoveSpeed", MoveSpeed);
				transform.DOMoveX(posX - stepRange, timeToReachPosition).OnComplete(() =>
				{
					MoveSpeed = 0f;
					animator.SetFloat("MoveSpeed", MoveSpeed);
				});

				transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else
			{
				// Move Right
				MoveSpeed = 1f;
				animator.SetFloat("MoveSpeed", MoveSpeed);
				transform.DOMoveX(posX + stepRange, timeToReachPosition).OnComplete(() =>
				{
					MoveSpeed = 0f;
					animator.SetFloat("MoveSpeed", MoveSpeed);
				});

				transform.localScale = new Vector3(-1f, 1f, 1f);
			}

			var waitTime = Random.Range(rangeWaitTime.x, rangeWaitTime.y);
			yield return new WaitForSeconds(waitTime);
		}

		yield break;
	}

}
