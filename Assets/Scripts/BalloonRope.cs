using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonRope : MonoBehaviour {

	public Transform ropeAnchorBalloon;
	public Transform ropeAnchorBoat;

	public LineRenderer lineRend;

	private void Update() {
		lineRend.SetPosition(0, ropeAnchorBalloon.position);
		lineRend.SetPosition(1, ropeAnchorBoat.position);
	}
}
