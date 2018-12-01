﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherInterractable : Interractible
{
	public float impulseForce = 5f;

	public override void Interract()
	{
		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, impulseForce), ForceMode2D.Impulse);
	}
}
