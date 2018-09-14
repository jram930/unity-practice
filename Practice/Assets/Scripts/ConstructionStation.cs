using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionStation : MonoBehaviour {

	GameObject constructionPanel;

	void Awake() {
		this.constructionPanel = GameObject.Find("ConstructionPanel");
	}

	// Use this for initialization
	void Start () {
		this.constructionPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		GameState gameState = GameState.Instance;
		gameState.SelectedConstructionStation = this.gameObject;
		this.constructionPanel.SetActive(true);
	}
}
