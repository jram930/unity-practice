using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

	public Transform hexPrefab;
	public int mapWidth = 11;
	public int mapHeight = 11;
	private const float scale = 50f;
	private float hexWidth = 1f * scale;
	private float hexHeight = 1.15528f * scale;
	public float gap = 0f;
	public Vector3 startPos;

	void Start() {
		float gappedWidth = hexWidth + gap;
		float gappedHeight = hexHeight + gap;
		for(int x=0; x<mapWidth; x++) {
			for(int z=0; z<mapHeight; z++) {
				float xPos = startPos.x + ((1.5f * gappedWidth) * (float)x);
				float zPos = startPos.z + ((1.12f * gappedHeight) * (float)z);
				if (z%2!=0) {
					xPos += (0.75f*gappedWidth);
				}
				Transform hex = Instantiate(hexPrefab) as Transform;
				Vector3 hexPos = new Vector3(xPos, 0, zPos);
				hex.position = hexPos;
				hex.parent = this.transform;
				hex.name = "Hex_"+x+"_"+z;
			}
		}
	}
}
