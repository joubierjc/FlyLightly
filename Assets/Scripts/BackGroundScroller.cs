using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour {

	public float scrollSpeed;
	public float verticalModifier;
	private Vector2 savedOffset;

	private Renderer rend;

	private void Awake() {
		rend = GetComponent<Renderer>();
	}

	private void Start() {
		savedOffset = rend.sharedMaterial.GetTextureOffset("_MainTex");
	}

	private void Update() {
		var x = Mathf.Repeat(Time.unscaledTime * scrollSpeed, 1);
		var y = Mathf.Repeat(GameManager.Instance.shipHeight * scrollSpeed * verticalModifier, 1);

		Vector2 offset = new Vector2(x, y);
		rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}

	private void OnDisable() {
		rend.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
	}
}
