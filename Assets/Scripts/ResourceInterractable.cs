using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInterractable : Interractable {
	public ResourceType resource;

	public override void Interract() {
		PlayerController.Instance.currentResource = resource;
	}
}
