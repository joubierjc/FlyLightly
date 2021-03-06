﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersSpawn : MonoBehaviour {

	[SerializeField] GameObject[] othersPrefab;
	[SerializeField] float impulseForceOnSpawn = 5f;

	[Header("Limits")]
	[SerializeField] Vector2 spawnLeft;
	[SerializeField] Vector2 spawnRight;


	public void StartSpawn()
	{
		StartCoroutine(Spawn());
	}

	public void StopSpawn()
	{
		StopCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		yield return new WaitForSeconds(GameManager.Instance.SpawnTimeOther);

		while (enabled) {
			SpawnOther();
			yield return new WaitForSeconds(GameManager.Instance.SpawnTimeOther);
		}

		yield break;
	}

	private void SpawnOther() {
		float spawnPointX;

		int otherIndex = Random.Range(0, othersPrefab.Length);

		spawnPointX = Random.Range(spawnLeft.x, spawnRight.x);
		Rigidbody2D otherRB = Instantiate(othersPrefab[otherIndex], new Vector3(spawnPointX, spawnLeft.y), Quaternion.identity).GetComponent<Rigidbody2D>();
		otherRB.AddForce(new Vector2(0, impulseForceOnSpawn), ForceMode2D.Impulse);
	}
}
