﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
	None,
	Ammo,
	Oil,
	Food,
	Coffee
}

public class FriendInterractable : Interractable {

	public ResourceType ressource;

	public override void Interract() {
		if (PlayerController.Instance.currentResource == ressource) {
			GetComponent<Health>().RestoreHealth();
			PlayerController.Instance.currentResource = ResourceType.None;
		}
	}
}
