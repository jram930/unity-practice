using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlanet : Planet {

	protected override void initPlanetTraits() {
		GameState gameState = GameState.Instance;
		this.planetType = "Gas Planet";
		this.planetCapacity = gameState.GetRandomInt(1, 4);
	}

}
