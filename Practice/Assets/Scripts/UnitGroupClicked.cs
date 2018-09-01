using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroupClicked : MonoBehaviour {

	WorldViewState gameState = WorldViewState.Instance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp() {
		print("I was clicked!");
		gameState.SelectedUnit = this.gameObject;
	}
}
