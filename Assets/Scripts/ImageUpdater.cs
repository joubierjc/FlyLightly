using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUpdater : MonoBehaviour {
	public Image[] imagesToUpdate;
	public Color toColor;

	public void ApplyChange() {
		for (int i = 0; i < imagesToUpdate.Length; i++) {
			imagesToUpdate[i].color = toColor;
		}
	}
}
