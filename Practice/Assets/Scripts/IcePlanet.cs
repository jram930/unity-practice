using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlanet : Planet {

	protected override void initPlanetTraits() {
		GameState gameState = GameState.Instance;
		this.planetType = "Ice Planet";
		this.planetCapacity = gameState.GetRandomInt(2, 5);
	}

}
