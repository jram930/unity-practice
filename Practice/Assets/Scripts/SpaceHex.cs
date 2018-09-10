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
		GameState gameState = GameState.Instance;
		this.spawner = this.transform.Find("SpaceHexSpawner").gameObject;
		this.SpawnSun();
		this.SpawnPlanets();
	}

	void SpawnSun() {
		GameState gameState = GameState.Instance;
		this.spawner = this.transform.Find("SpaceHexSpawner").gameObject;
		int sunProb = gameState.GetRandomInt(0, 100);
		this.hasSun = false;
		if(sunProb < 60) {
			hasSun = true;
		}

		if(hasSun) {
			GameObject sunPrefab = Resources.Load("prefabs/Sun", typeof(GameObject)) as GameObject;
			GameObject sun = Instantiate(sunPrefab);
			sun.name = "Sun";
			sun.transform.position = this.spawner.transform.position;
			sun.transform.parent = this.transform;
		}
	}

	private void SpawnNumberOfPlanets(int n) {
		this.planets = new GameObject[n];
		for (int i = 0; i < n; i++) {
			GameObject planetPrefab = Resources.Load("prefabs/Planet", typeof(GameObject)) as GameObject;
			GameObject planet = Instantiate(planetPrefab);
			planet.name = "Planet";
			this.planets[i] = planet;
			planet.transform.parent = this.transform;
			planet.transform.position = this.GetRandomPosition();
		}
	}

	private void SpawnPlanets() {
		GameState gameState = GameState.Instance;
		if(this.hasSun) {
			int planetProb = gameState.GetRandomInt(0, 100);
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

		this.SpawnNumberOfPlanets(this.numPlanets);
	}

	private Vector3 GetRandomPosition() {
		GameState gameState = GameState.Instance;
		this.spawner = this.transform.Find("SpaceHexSpawner").gameObject;
		float xTranslate = gameState.GetRandomFloat(10f, 25f);
		bool xPositive = gameState.GetRandomInt(0, 2) == 0;
		float yTranslate = gameState.GetRandomFloat(10f, 25f);
		bool yPositive = gameState.GetRandomInt(0, 2) == 0;
		float zTranslate = gameState.GetRandomFloat(10f, 25f);
		bool zPositive = gameState.GetRandomInt(0, 2) == 0;  
		float posX = xPositive ? this.spawner.transform.position.x + xTranslate : this.spawner.transform.position.x - xTranslate;
		float posY = yPositive ? this.spawner.transform.position.y + yTranslate : this.spawner.transform.position.y - yTranslate;
		float posZ = zPositive ? this.spawner.transform.position.z + zTranslate : this.spawner.transform.position.z - zTranslate;
		return new Vector3(posX, posY, posZ);
	}

	/// <summary>
	/// This object doesn't have a collider, but its internal model does.
	/// The model's collider routes click events here to be handled appropriately.
	/// </summary>
	public void HandleClick() {
		this.PlanSquadronMove();
		this.DeselectUnit();
		this.RemoveHexStates();
	}

	private void DeselectUnit() {
		GameState gameState = GameState.Instance;
		gameState.SelectedUnit.GetComponent<Squadron>().DeselectUnit();
	}

	private void PlanSquadronMove() {
		GameState gameState = GameState.Instance;
		gameState.SelectedUnit.transform.parent = this.transform;
		GameObject squadron = this.transform.Find(gameState.SelectedUnit.name).gameObject;
		Material plannedMoveMaterial = Resources.Load<Material>("materials/PlannedMove");
		Vector3 dest = this.GetRandomPosition();
		LineRenderer lineRenderer = squadron.GetComponent<LineRenderer>();
		if(lineRenderer == null) {
			lineRenderer = squadron.AddComponent<LineRenderer>();
		}
		lineRenderer.SetPosition(0, squadron.transform.position);
		lineRenderer.SetPosition(1, dest);
		lineRenderer.material = plannedMoveMaterial;
		Squadron squadronLogic = squadron.GetComponent<Squadron>();
		squadronLogic.nextPosition = dest;
		squadronLogic.nextParent = this.gameObject;
	}

	private void RemoveHexStates() {
		Material deselectedMaterial = Resources.Load<Material>("materials/HexDeselected");
		GameObject[] spaceHexes = GameObject.FindGameObjectsWithTag("SpaceHex");
		foreach(GameObject spaceHex in spaceHexes) {
			GameObject spaceHexModel = spaceHex.transform.Find("SpaceHexModel").gameObject;
			spaceHexModel.GetComponent<MeshCollider>().enabled = false;
			spaceHexModel.GetComponent<MeshRenderer>().material = deselectedMaterial;
		}
	}

	public void MakeHomeHex() {
		this.SpawnHomeSunsAndPlanets();
		this.SpawnHomeStructures();
		this.SpawnHomeSquadrons();
	}

	private void SpawnHomeSunsAndPlanets() {
		GameState gameState = GameState.Instance;
		Transform sunFind = this.transform.Find("Sun");
		if (sunFind == null) {
			this.SpawnSun();
		}
		this.numPlanets = gameState.GetRandomInt(2, 5);
		int numChildren = this.transform.childCount;
		List<int> planetsToDestroy = new List<int>();
		for (int i = 0; i < numChildren; i++) {
			Transform child = this.transform.GetChild(i);
			if (child.tag == "Planet") {
				planetsToDestroy.Add(i);
			}
		}
		for (int index = planetsToDestroy.Count - 1; index >= 0; index--) {
			Destroy(this.transform.GetChild(planetsToDestroy[index]).gameObject);
		}
		this.SpawnNumberOfPlanets(this.numPlanets);
	}

	private void SpawnHomeStructures() {
		this.SpawnConstructionStation();
		this.SpawnResearchStation();
		this.SpawnDefenseStation();
	}

	private void SpawnConstructionStation() {
		GameObject constructionStationPrefab = Resources.Load<GameObject>("prefabs/ConstructionStation");
		GameObject constructionStation = Instantiate(constructionStationPrefab);
		constructionStation.name = "ConstructionStation";
		constructionStation.transform.parent = this.transform;
		constructionStation.transform.position = this.GetRandomPosition();
	}

	private void SpawnResearchStation() {
		GameObject researchStationPrefab = Resources.Load<GameObject>("prefabs/ResearchStation");
		GameObject researchStation = Instantiate(researchStationPrefab);
		researchStation.name = "ResearchStation";
		researchStation.transform.parent = this.transform;
		researchStation.transform.position = this.GetRandomPosition();
	}

	private void SpawnDefenseStation() {
		GameObject defenseStationPrefab = Resources.Load<GameObject>("prefabs/DefenseStation");
		GameObject defenseStation = Instantiate(defenseStationPrefab);
		defenseStation.name = "DefenseStation";
		defenseStation.transform.parent = this.transform;
		defenseStation.transform.position = this.GetRandomPosition();
	}

	private void SpawnHomeSquadrons() {
		this.SpawnExplorerSquadron();
		this.SpawnFighterSquadron();
	}

	private void SpawnExplorerSquadron() {
		GameState gameState = GameState.Instance;
		this.spawner = this.transform.Find("SpaceHexSpawner").gameObject;
		GameObject squadronPrefab = Resources.Load("prefabs/ExplorerSquadron", typeof(GameObject)) as GameObject;
		GameObject squadron = Instantiate(squadronPrefab);
		squadron.transform.parent = this.transform;
		squadron.transform.position = this.GetRandomPosition();
		squadron.name = "ExplorerSquadron";
	}

	private void SpawnFighterSquadron() {
		GameState gameState = GameState.Instance;
		this.spawner = this.transform.Find("SpaceHexSpawner").gameObject;
		GameObject squadronPrefab = Resources.Load("prefabs/FighterSquadron", typeof(GameObject)) as GameObject;
		GameObject squadron = Instantiate(squadronPrefab);
		squadron.transform.parent = this.transform;
		squadron.transform.position = this.GetRandomPosition();
		squadron.name = "FighterSquadron";
	}
}
