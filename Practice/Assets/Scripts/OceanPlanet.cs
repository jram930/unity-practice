using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanPlanet : Planet {

	protected override void initPlanetTraits() {
		GameState gameState = GameState.Instance;
		this.planetType = "Ocean Planet";
		this.planetCapacity = gameState.GetRandomInt(2, 5);
	}

}
