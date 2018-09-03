using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour {

	private bool selected = false;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SphereCollider>().material = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		if (!this.selected) {
			this.ShowSelected();
			this.HighlightCurrentHex();
			this.ShowPossibleMoves();
		}
	}

	private void ShowSelected() {
		this.selected = true;
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.parent = this.transform;
		sphere.transform.position = new Vector3(this.transform.position.x + 0.8f, this.transform.position.y + 2f, this.transform.position.z + 0.3f);
		sphere.transform.localScale = new Vector3(10, 10, 10);
		Material selectedUnitMaterial = Resources.Load<Material>("materials/SelectedUnit") as Material;
		sphere.GetComponent<MeshRenderer>().material = selectedUnitMaterial;
	}

	private void HighlightCurrentHex() {
		Material selectedMaterial = Resources.Load<Material>("materials/SelectedHex") as Material;
		GameObject parentHex = this.transform.parent.transform.gameObject;
		GameObject parentModel = parentHex.transform.Find("SpaceHexModel").gameObject;
		parentModel.GetComponent<MeshRenderer>().material = selectedMaterial;
	}

	private void ShowPossibleMoves() {
		GameObject parentHex = this.transform.parent.transform.gameObject;
		string[] nameTokens = parentHex.name.Split('_');
		int parentX = Int32.Parse(nameTokens[1]);
		int parentZ = Int32.Parse(nameTokens[2]);
		print("Current hex coordinates are " + parentX + " and " + parentZ);
	}
}
