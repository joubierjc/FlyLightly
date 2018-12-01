using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersSpawn : MonoBehaviour {
	
	[SerializeField] GameObject otherPrefab;
	[SerializeField] float spawnTime = 2f;
	[SerializeField] float impulseForceOnSpawn = 5f;
	
	[SerializeField] Vector2 spawnLeft;
	[SerializeField] Vector2 spawnRight;
	[SerializeField] Transform[] spawnPoints;

	void Start()
	{
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		SpawnOther();
		yield return new WaitForSeconds(spawnTime);

		while (enabled)
		{
			SpawnOther();
			yield return new WaitForSeconds(spawnTime);
		}

		yield break;
	}

	private void SpawnOther()
	{
		float spawnPointX;

		spawnPointX = Random.Range(spawnLeft.x, spawnRight.x);
		Rigidbody2D otherRB = Instantiate(otherPrefab, new Vector3(spawnPointX, spawnLeft.y), Quaternion.identity).GetComponent<Rigidbody2D>();
		otherRB.AddForce(new Vector2(0,impulseForceOnSpawn), ForceMode2D.Impulse);
	}
}
