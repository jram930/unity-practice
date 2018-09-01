using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectedPanel : MonoBehaviour {

	WorldViewState gameState = WorldViewState.Instance;

	public Text UnitSelectedText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameState.SelectedUnit != null) {
			this.UnitSelectedText.text = gameState.SelectedUnit.name;
		}
	}
}
