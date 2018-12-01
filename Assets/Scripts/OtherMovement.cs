using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OtherMovement : MonoBehaviour {

	[SerializeField] float waitTime = 2f;
	[SerializeField] float timetoReachPosition = 1f;
	[SerializeField] float stepRange = .5f;
	[SerializeField] float leftLimit;
	[SerializeField] float rightLimit;
	// Use this for initialization
	void Start () {
		StartCoroutine(Move());
	}

	IEnumerator Move()
	{
		while(enabled)
		{
			float posX = transform.position.x;
			if (posX+stepRange < rightLimit && posX-stepRange > leftLimit)
				{
					float random = Random.value;
					if(random < 0.5f)
					{
						//Move Left
						transform.DOMoveX(posX - stepRange, timetoReachPosition);
					}
					else
					{
						//Move Right
						transform.DOMoveX(posX + stepRange, timetoReachPosition);
					}
				}
			else if(posX+stepRange > rightLimit)
				{
					//Move Left
					transform.DOMoveX(posX - stepRange, timetoReachPosition);
				}
			else
				{
					// Move Right
					transform.DOMoveX(posX + stepRange, timetoReachPosition);
				}


			yield return new WaitForSeconds(waitTime);
		}

		yield break;
	}
	
}
