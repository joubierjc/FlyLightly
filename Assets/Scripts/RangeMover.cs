using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RangeMover : MonoBehaviour {

	public Vector2 rangeX = new Vector2(-2f, 2f);
	public Vector2 rangeY = new Vector2(-1f, 1f);
	public float durationX = 3f;
	public float durationY = 4f;

	public void Awake() {
		MoveToMaxX();
		MoveToMaxY();
	}

	private void MoveToMaxX() {
		transform.DOLocalMoveX(rangeX.y, durationX).OnComplete(MoveToMinX);
	}

	private void MoveToMaxY() {
		transform.DOLocalMoveY(rangeY.y, durationY).OnComplete(MoveToMinY);
	}

	private void MoveToMinX() {
		transform.DOLocalMoveX(rangeX.x, durationX).OnComplete(MoveToMaxX);
	}

	private void MoveToMinY() {
		transform.DOLocalMoveY(rangeY.x, durationY).OnComplete(MoveToMaxY);
	}
}
