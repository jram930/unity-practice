using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {

	/// <summary>
	/// Ends the player's turn.
	/// </summary>
	public void DoEndTurn() {
		this.MoveUnits();
	}

	private void MoveUnits() {
		GameState gameState = GameState.Instance;
		Destroy(gameState.SelectedUnit.GetComponent<LineRenderer>());
		gameState.SelectedUnit.transform.parent = gameState.UnitDestinationHex.transform;
		gameState.SelectedUnit.transform.position = gameState.UnitDestinaionPosition;
	}
}
