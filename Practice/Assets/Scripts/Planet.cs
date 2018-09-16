using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planet : MonoBehaviour {

	public GameObject PlanetPanel;
	private string planetName;
	protected int planetCapacity;
	protected string planetType;

	void Awake() {
		this.PlanetPanel = GameObject.Find("PlanetPanel");
	}

	// Use this for initialization
	void Start () {
		GameState gameState = GameState.Instance;
		this.PlanetPanel.SetActive(false);
		this.planetName = PlanetNameGenerator.Instance.GetRandomPlanetName();
		this.initPlanetTraits();
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

	public int GetPlanetCapacity() {
		return this.planetCapacity;
	}

	public string GetPlanetType() {
		return this.planetType;
	}

	protected abstract void initPlanetTraits();
}
