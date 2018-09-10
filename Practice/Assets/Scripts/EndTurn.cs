﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {

	/// <summary>
	/// Ends the player's turn.
	/// </summary>
	public void DoEndTurn() {
		this.MoveUnits();
	}

	private void MoveUnits() {
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		foreach(GameObject unit in units) {
			Squadron squadron = unit.GetComponent<Squadron>();
			LineRenderer line = unit.GetComponent<LineRenderer>();
			if(squadron.nextParent != null && squadron.nextPosition != null && line != null) {
				squadron.transform.position = (Vector3) squadron.nextPosition;
				squadron.transform.parent = squadron.nextParent.transform;
				Destroy(line);
				squadron.nextParent = null;
				squadron.nextPosition = null;
			}
		}
	}
}
