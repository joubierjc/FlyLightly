using System.Collections;
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
	public float karmaGains = 200f;

	public override void Interract() {
		if (PlayerController.Instance.currentResource == ressource) {
			switch (ressource)
			{
				case ResourceType.Ammo:
					GameManager.Instance.audioManager.Play("reload-gunner");
					break;
				case ResourceType.Oil:
					GameManager.Instance.audioManager.Play("ok-engineer");
					break;
				case ResourceType.Food:
					GameManager.Instance.audioManager.Play("yeah-cook");
					break;
				case ResourceType.Coffee:
					GameManager.Instance.audioManager.Play("slurp-commander");
					break;
			}

			GetComponent<Health>().RestoreHealth();
			PlayerController.Instance.currentResource = ResourceType.None;
			GameManager.Instance.karma += karmaGains;
		}
	}
}
