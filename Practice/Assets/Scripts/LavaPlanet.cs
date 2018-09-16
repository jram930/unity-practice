using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlanet : Planet {

	protected override void initPlanetTraits() {
		GameState gameState = GameState.Instance;
		this.planetType = "Lava Planet";
		this.planetCapacity = gameState.GetRandomInt(2, 5);
	}

}
