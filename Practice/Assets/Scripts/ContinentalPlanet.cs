using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinentalPlanet : Planet {

	protected override void initPlanetTraits() {
		GameState gameState = GameState.Instance;
		this.planetType = "Continental Planet";
		this.planetCapacity = gameState.GetRandomInt(4, 9);
	}

}
