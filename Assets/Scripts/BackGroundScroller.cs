using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour {

	public float scrollSpeed;
	private Vector2 savedOffset;

	private Renderer rend;

	private void Awake() {
		rend = GetComponent<Renderer>();
	}

	private void Start() {
		savedOffset = rend.sharedMaterial.GetTextureOffset("_MainTex");
	}

	private void Update() {
		float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2(x, savedOffset.y);
		rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}

	private void OnDisable() {
		rend.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
	}
}
