﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Squadron : MonoBehaviour {

	private bool selected = false;
	private GameState gameState;
	private GameObject selectedSphere;
	public Vector3? nextPosition = null;
	public GameObject nextParent = null;
	private Boolean moving = false;

	protected abstract int GetMovementSpeed();

	// Use this for initialization
	void Start() {
		this.gameObject.GetComponent<SphereCollider>().material = null;
	}

	void Update() {
		if(this.moving && this.transform.position != nextPosition) {
			float speed = 100f * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards(this.transform.position, (Vector3) this.nextPosition, speed);
		} else if(this.moving){
			this.moving = false;
			this.transform.parent = this.nextParent.transform;
			this.nextPosition = null;
			this.nextParent = null;
		}
	}

	public void DeselectUnit() {
		GameState gameState = GameState.Instance;
		this.selected = false;
		Destroy(this.selectedSphere);
		gameState.SelectedUnit = null;
	}

	public void MoveToNextPosition() {
		this.moving = true;
	}

	/// <summary>
	/// Handles a click on this unit.
	/// Does the following if it isn't already selected:
	/// - shows it is selected
	/// - highlights the current hex
	/// - highlights possible hex movements
	/// - displays the unit name in the toolbar
	/// </summary>
	void OnMouseDown() {
		GameState gameState = GameState.Instance;
		if (!this.selected) {
			if(gameState.SelectedUnit != null) {
				gameState.SelectedUnit.GetComponent<Squadron>().DeselectUnit();
			}
			gameState.SelectedUnit = this.transform.gameObject;
			this.SelectUnit();
			this.HighlightCurrentHex();
			this.ShowPossibleMoves();
		}
	}

	/// <summary>
	/// Adds a visual indication that this unit is currently selected (Adds a transparent sphere around it)
	/// </summary>
	private void SelectUnit() {
		this.selected = true;
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		this.selectedSphere = sphere;
		sphere.transform.parent = this.transform;
		sphere.transform.position = new Vector3(this.transform.position.x + 0.8f, this.transform.position.y + 2f, this.transform.position.z + 0.3f);
		sphere.transform.localScale = new Vector3(10, 10, 10);
		Material selectedUnitMaterial = Resources.Load<Material>("materials/SelectedUnit");
		sphere.GetComponent<MeshRenderer>().material = selectedUnitMaterial;
	}

	/// <summary>
	/// Displays the unit's information in the toolbar.
	/// </summary>
	private void ShowSelectedInToolbar() {
		GameObject selectedNameText = GameObject.Find("SelectedUnitNameText");
		selectedNameText.GetComponent<Text>().text = this.transform.gameObject.name;
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
