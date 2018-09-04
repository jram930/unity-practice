using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour {

	private bool selected = false;
	private GameState gameState;

	// Use this for initialization
	void Start() {
		this.gameObject.GetComponent<SphereCollider>().material = null;
	}

	// Update is called once per frame
	void Update() {

	}

	/// <summary>
	/// Handles a click on this unit.
	/// Does the following if it isn't already selected:
	/// - shows it is selected
	/// - highlights the current hex
	/// - highlights possible hex movements
	/// </summary>
	void OnMouseDown() {
		this.gameState = GameState.Instance;
		if (!this.selected) {
			this.gameState.SelectedUnit = this.transform.gameObject;
			this.ShowSelected();
			this.HighlightCurrentHex();
			this.ShowPossibleMoves();
		}
	}

	/// <summary>
	/// Adds a visual indication that this unit is currently selected (Adds a transparent sphere around it)
	/// </summary>
	private void ShowSelected() {
		this.selected = true;
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.parent = this.transform;
		sphere.transform.position = new Vector3(this.transform.position.x + 0.8f, this.transform.position.y + 2f, this.transform.position.z + 0.3f);
		sphere.transform.localScale = new Vector3(10, 10, 10);
		Material selectedUnitMaterial = Resources.Load<Material>("materials/SelectedUnit");
		sphere.GetComponent<MeshRenderer>().material = selectedUnitMaterial;
	}

	/// <summary>
	/// Highlights the hex this unit is currently in.
	/// </summary>
	private void HighlightCurrentHex() {
		Material selectedMaterial = Resources.Load<Material>("materials/SelectedHex");
		GameObject parentHex = this.transform.parent.transform.gameObject;
		GameObject parentModel = parentHex.transform.Find("SpaceHexModel").gameObject;
		parentModel.GetComponent<MeshRenderer>().material = selectedMaterial;
	}

	/// <summary>
	/// Highlights the hexes this unit is allowed to move to.
	/// </summary>
	private void ShowPossibleMoves() {
		this.gameState = GameState.Instance;
		GameObject parentHex = this.transform.parent.transform.gameObject;
		string[] nameTokens = parentHex.name.Split('_');
		int parentX = Int32.Parse(nameTokens[1]);
		int parentZ = Int32.Parse(nameTokens[2]);
		List<string> possibleMoves = new List<string>();

		bool remainingX = false;
		bool previousX = false;
		bool remainingZ = false;
		bool previousZ = false;
		if (parentX > 0) {
			previousX = true;
			possibleMoves.Add("Hex_" + (parentX - 1) + "_" + parentZ);
		}
		if (parentX < this.gameState.MapWidth) {
			remainingX = true;
			possibleMoves.Add("Hex_" + (parentX + 1) + "_" + parentZ);
		}
		if (parentZ > 0) {
			previousZ = true;
			possibleMoves.Add("Hex_" + parentX + "_" + (parentZ - 1));
		}
		if (parentZ < this.gameState.MapHeight) {
			remainingZ = true;
			possibleMoves.Add("Hex_" + parentX + "_" + (parentZ + 1));
		}
		if (remainingX && remainingZ && parentZ % 2 != 0) {
			possibleMoves.Add("Hex_" + (parentX + 1) + "_" + (parentZ + 1));
		}
		if (remainingX && previousZ && parentZ % 2 != 0) {
			possibleMoves.Add("Hex_" + (parentX + 1) + "_" + (parentZ - 1));
		}
		if (previousX && remainingZ && parentZ % 2 == 0) {
			possibleMoves.Add("Hex_" + (parentX - 1) + "_" + (parentZ + 1));
		}
		if (previousX && previousZ && parentZ % 2 == 0) {
			possibleMoves.Add("Hex_" + (parentX - 1) + "_" + (parentZ - 1));
		}

		Material possibleHexMaterial = Resources.Load<Material>("materials/PossibleHex");
		foreach (string possibleMove in possibleMoves) {
			GameObject possibleHex = GameObject.Find(possibleMove);
			GameObject possibleHexModel = possibleHex.transform.Find("SpaceHexModel").gameObject;
			possibleHexModel.GetComponent<MeshRenderer>().material = possibleHexMaterial;
			possibleHexModel.GetComponent<MeshCollider>().enabled = true;
		}
	}
}
