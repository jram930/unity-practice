using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenu : MonoBehaviour {

	public GameObject Menu;
	public Text PlanetName;
	public Text PlanetType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameState gameState = GameState.Instance;
		if(gameState.SelectedPlanet == null) {
			this.Menu.SetActive(false);
		} else {
			Planet planet = gameState.SelectedPlanet.GetComponent<Planet>();
			PlanetName.text = planet.GetPlanetName();
			PlanetType.text = planet.GetPlanetType();
		}
	}

	public void CloseClick() {
		GameState gameState = GameState.Instance;
		if(this.Menu != null) {
			gameState.SelectedPlanet = null;
		}
	}
}
