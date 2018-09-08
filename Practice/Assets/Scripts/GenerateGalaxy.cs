﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GenerateGalaxy : MonoBehaviour {

	public Transform hexPrefab;

	public int MapWidth = 11;
	public int MapHeight = 11;
	public float Gap = 0f;
	public Vector3 StartPos;
	private const float scale = 57.8f;
	private float hexWidth = 1f * scale;
	private float hexHeight = 1.15528f * scale;
	private GameObject[][] hexes;
	private GameState gameState;

	void Start() {
		this.hexes = new GameObject[MapWidth][];
		for(int x=0; x<this.MapWidth; x++) {
			this.hexes[x] = new GameObject[this.MapHeight];
		}
		this.GenerateMap();
		this.GenerateStartingHex();
		//this.SpawnUnits();
	}

	private void GenerateMap() {
		this.gameState = GameState.Instance;
		this.gameState.MapHeight = this.MapHeight;
		this.gameState.MapWidth = this.MapWidth;
		float gappedWidth = hexWidth + Gap;
		float gappedHeight = hexHeight + Gap;
		for (int x = 0; x < MapWidth; x++) {
			for (int z = 0; z < MapHeight; z++) {
				float xPos = StartPos.x + ((1.5f * gappedWidth) * (float)x);
				float zPos = StartPos.z + ((1.12f * gappedHeight) * (float)z);
				if (z % 2 != 0) {
					xPos += (0.75f * gappedWidth);
				}
				Transform hex = Instantiate(hexPrefab) as Transform;
				Vector3 hexPos = new Vector3(xPos, -12.5f, zPos);
				hex.position = hexPos;
				hex.parent = this.transform;
				hex.name = "Hex_" + x + "_" + z;
				hexes[x][z] = hex.gameObject;
			}
		}
	}

	private void GenerateStartingHex() {
		GameState gameState = GameState.Instance;
		// For now, just start at 0,0
		GameObject startingHex = GameObject.Find("Hex_0_0");
		SpaceHex hex = startingHex.GetComponent<SpaceHex>();
		hex.MakeHomeHex();
	}

	private void SpawnUnits() {
		SpaceHex spaceHex = this.hexes[2][2].GetComponent<SpaceHex>();
		spaceHex.SpawnFleet();
	}
}