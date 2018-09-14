using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planet : MonoBehaviour {

	public GameObject PlanetPanel;

	void Awake() {
		this.PlanetPanel = GameObject.Find("PlanetPanel");
	}

	// Use this for initialization
	void Start () {
		this.PlanetPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		GameState gameState = GameState.Instance;
		gameState.SelectedPlanet = this.gameObject;
		this.PlanetPanel.SetActive(true);
	}

	public string GetPlanetName() {
		return "Todo";
	}

	public abstract string GetPlanetType();
}
