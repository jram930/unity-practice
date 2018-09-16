using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetMenu : MonoBehaviour {

	public GameObject Menu;
	public Text PlanetName;
	public Text PlanetType;
	public Text PlanetCapacity;
	public GameObject[] PlanetWorkerTiles;

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
			PlanetCapacity.text = planet.GetPlanetCapacity().ToString();
			this.ShowRelevantWorkerTiles(planet.GetPlanetCapacity());
		}
	}

	private void ShowRelevantWorkerTiles(int capacity) {
		for(int i=0; i<PlanetWorkerTiles.Length; i++) {
			PlanetWorkerTiles[i].SetActive(true);
		}
		for(int i=((PlanetWorkerTiles.Length-1) - capacity); i>=0; i--) {
			int tileToHide = PlanetWorkerTiles.Length - i - 1;
			PlanetWorkerTiles[tileToHide].SetActive(false);
		}
	}

	public void CloseClick() {
		GameState gameState = GameState.Instance;
		if(this.Menu != null) {
			gameState.SelectedPlanet = null;
		}
	}
}
