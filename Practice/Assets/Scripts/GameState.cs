﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {

	public GameObject SelectedUnit { get; set; }

	private static GameState instance = null;

	private GameState() {
		Random.InitState((int)System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1)).TotalSeconds);
	}

	public static GameState Instance {
		get {
			if(instance == null) {
				instance = new GameState();
			}
			return instance;
		}
	}

	public float GetRandomFloat(float start, float end) {
		return Random.Range(start, end);
	}

	public int GetRandomInt(int start, int end) {
		return Random.Range(start, end);
	}
}