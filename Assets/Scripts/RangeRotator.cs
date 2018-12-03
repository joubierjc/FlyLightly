using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RangeRotator : MonoBehaviour {

	public Vector2 range = new Vector2(-10f, 10f);
	public float duration = 2f;

	public void Awake() {
		RotateToMax();
	}

	private void RotateToMax() {
		transform.DORotate(new Vector3(0f, range.y, 0f), duration).OnComplete(RotateToMin);
	}

	private void RotateToMin() {
		transform.DORotate(new Vector3(0f, range.x, 0f), duration).OnComplete(RotateToMax);
	}
}
