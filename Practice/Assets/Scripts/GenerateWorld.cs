using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour {

	public Transform hexPrefab;

	public int mapWidth = 11;
	public int mapHeight = 11;
	private const float scale = 50f;
	private float hexWidth = 1f * scale;
	private float hexHeight = 1.15528f * scale;
	public float gap = 0f;
	public Vector3 startPos;

	// 0 = grassland
	// 1 = mountain
	// 2 = water
	// 3 = plain
	private const int NUM_HEX_TYPES = 4;

	// 0 = soldier
	private const int NUM_UNIT_TYPES = 1;

	public Material[] hexMaterials;
	public Material[] unitMaterials;

	void Start() {
		//this.generateMap();
		//this.spawnUnits();
	}

	private void generateMap() {
		float gappedWidth = hexWidth + gap;
		float gappedHeight = hexHeight + gap;
		for (int x = 0; x < mapWidth; x++) {
			for (int z = 0; z < mapHeight; z++) {
				float xPos = startPos.x + ((1.5f * gappedWidth) * (float)x);
				float zPos = startPos.z + ((1.12f * gappedHeight) * (float)z);
				if (z % 2 != 0) {
					xPos += (0.75f * gappedWidth);
				}
				Transform hex = Instantiate(hexPrefab) as Transform;
				Vector3 hexPos = new Vector3(xPos, -12.5f, zPos);
				hex.position = hexPos;
				hex.parent = this.transform;
				hex.name = "Hex_" + x + "_" + z;

				int hexType = Random.Range(0, NUM_HEX_TYPES);

				Material mat = null;
				switch (hexType) {
					case 0:
						mat = this.hexMaterials[0];
						break;
					case 1:
						mat = this.hexMaterials[1];
						break;
					case 2:
						mat = this.hexMaterials[2];
						break;
					case 3:
						mat = this.hexMaterials[3];
						break;
					default:
						print("Invalid material selected");
						break;
				}

				if (mat != null) {
					hex.gameObject.GetComponent<MeshRenderer>().material = mat;
				}
			}
		}
	}

	private void spawnUnits() {
		GameObject unit = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		unit.GetComponent<MeshRenderer>().material = unitMaterials[0];
		unit.transform.position = new Vector3(0, unit.GetComponent<MeshFilter>().mesh.bounds.extents.y, 0);
		//print(unit.GetComponent<MeshFilter>().mesh.bounds.extents.y);
	}
}
