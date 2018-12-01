using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersSpawn : MonoBehaviour {
	
	[SerializeField] GameObject otherPrefab;
	[SerializeField] float spawnTime = 2f;
	[SerializeField] Transform[] spawnPoints;
	private List<GameObject> enemies = new List<GameObject>();

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

			int randSpawn = Random.Range(0, spawnPoints.Length);
			
			yield return new WaitForSeconds(spawnTime);
		}

		yield break;
	}

	private void SpawnOther()
	{
		int spawnPointIndex;

		spawnPointIndex = Random.Range(0, spawnPoints.Length);
		GameObject enn = Instantiate(otherPrefab, spawnPoints[spawnPointIndex]);
		enemies.Add(enn);
	}
}
