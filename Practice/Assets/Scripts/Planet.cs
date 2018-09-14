using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planet : MonoBehaviour {

	public GameObject PlanetPanel;
	private string planetName;

	void Awake() {
		this.PlanetPanel = GameObject.Find("PlanetPanel");
	}

	// Use this for initialization
	void Start () {
		this.PlanetPanel.SetActive(false);
		this.planetName = PlanetNameGenerator.Instance.GetRandomPlanetName();
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
		return this.planetName;
	}

	public abstract string GetPlanetType();
}
