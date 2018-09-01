using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexClicked : MonoBehaviour {

	public Material InactiveMaterial;
	public Material ActiveMaterial;
	private bool isActive = false;

	WorldViewState gameState = WorldViewState.Instance;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<MeshRenderer>().material = InactiveMaterial;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		Debug.Log("Hex was clicked!");
		this.isActive = !this.isActive;
		if(this.isActive) {
			this.gameObject.GetComponent<MeshRenderer>().material = this.ActiveMaterial;
		} else {
			this.gameObject.GetComponent<MeshRenderer>().material = this.InactiveMaterial;
		}

	}
}
