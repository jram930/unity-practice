using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldViewState {

	public GameObject SelectedUnit { get; set; }

	private static WorldViewState instance = null;

	private WorldViewState() {}

	public static WorldViewState Instance {
		get {
			if(instance == null) {
				instance = new WorldViewState();
			}
			return instance;
		}
	}
}
