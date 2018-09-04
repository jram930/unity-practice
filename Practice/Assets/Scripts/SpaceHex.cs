using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHex : MonoBehaviour {

	private GameObject spawner;
	private bool hasSun = false;
	private int numPlanets = 0;
	private GameObject[] planets;
	private GameState gameState;

	// Use this for initialization
	void Start () {
		this.gameState = GameState.Instance;
		this.spawner = this.transform.Find("SpaceHexSpawner").gameObject;
		this.SpawnSun();
		this.SpawnPlanets();
	}

	void SpawnSun() {

		int sunProb = this.gameState.GetRandomInt(0, 100);
		this.hasSun = false;
		if(sunProb < 60) {
			hasSun = true;
		}

		if(hasSun) {
			GameObject sunPrefab = Resources.Load("prefabs/Sun", typeof(GameObject)) as GameObject;
			GameObject sun = Instantiate(sunPrefab);
			sun.transform.position = this.spawner.transform.position;
			sun.transform.parent = this.transform;
		}
	}

	void SpawnPlanets() {

		if(this.hasSun) {
			int planetProb = this.gameState.GetRandomInt(0, 100);
			if (planetProb < 5) {
				this.numPlanets = 7;
			} else if(planetProb < 10) {
				this.numPlanets = 6;
			} else if(planetProb < 20) {
				this.numPlanets = 5;
			} else if(planetProb < 30) {
				this.numPlanets = 4;
			} else if(planetProb < 60) {
				this.numPlanets = 3;
			} else if(planetProb < 85) {
				this.numPlanets = 2;
			} else if(planetProb < 95) {
				this.numPlanets = 1;
			} else {
				this.numPlanets = 0;
			}
		}

		this.planets = new GameObject[this.numPlanets];

		for(int i=0; i<this.numPlanets; i++) {
			GameObject planetPrefab = Resources.Load("prefabs/Planet", typeof(GameObject)) as GameObject;
			GameObject planet = Instantiate(planetPrefab);
			this.planets[i] = planet;
			planet.transform.parent = this.transform;
			planet.transform.position = this.GetRandomPosition();
		}
	}

	private Vector3 GetRandomPosition() {
		this.gameState = GameState.Instance;
		float xTranslate = this.gameState.GetRandomFloat(10f, 25f);
		bool xPositive = this.gameState.GetRandomInt(0, 2) == 0;
		float yTranslate = this.gameState.GetRandomFloat(10f, 25f);
		bool yPositive = this.gameState.GetRandomInt(0, 2) == 0;
		float zTranslate = this.gameState.GetRandomFloat(10f, 25f);
		bool zPositive = this.gameState.GetRandomInt(0, 2) == 0;
		float posX = xPositive ? this.spawner.transform.position.x + xTranslate : this.spawner.transform.position.x - xTranslate;
		float posY = yPositive ? this.spawner.transform.position.y + yTranslate : this.spawner.transform.position.y - yTranslate;
		float posZ = zPositive ? this.spawner.transform.position.z + zTranslate : this.spawner.transform.position.z - zTranslate;
		return new Vector3(posX, posY, posZ);
	}

	public void SpawnFleet() {
		this.gameState = GameState.Instance;
		this.spawner = this.transform.Find("SpaceHexSpawner").gameObject;
		GameObject fleetPrefab = Resources.Load("prefabs/Fleet", typeof(GameObject)) as GameObject;
		GameObject fleet = Instantiate(fleetPrefab);
		fleet.transform.parent = this.transform;
		fleet.transform.position = this.GetRandomPosition();
		fleet.gameObject.AddComponent<Fleet>();
	}

	/// <summary>
	/// This object doesn't have a collider, but its internal model does.
	/// The model's collider routes click events here to be handled appropriately.
	/// </summary>
	public void HandleClick() {
		GameState gameState = GameState.Instance;
		gameState.SelectedUnit.transform.parent = this.transform;
		GameObject fleet = this.transform.Find(gameState.SelectedUnit.name).gameObject;
		fleet.transform.position = this.GetRandomPosition();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
