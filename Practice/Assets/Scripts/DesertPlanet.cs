using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertPlanet : Planet {

	protected override void initPlanetTraits() {
		GameState gameState = GameState.Instance;
		this.planetType = "Desert Planet";
		this.planetCapacity = gameState.GetRandomInt(2, 5);
	}

}
