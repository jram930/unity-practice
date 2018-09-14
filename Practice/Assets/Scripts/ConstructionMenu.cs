using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionMenu : MonoBehaviour {

	public GameObject Menu;
	public Button BuildFighterButton;
	public Button BuildExplorerButton;

	private void Update() {
		PlayerState playerState = PlayerState.Instance;
		if(playerState.Materials < 600) {
			BuildFighterButton.interactable = false;
		}
		if(playerState.Materials < 500) {
			BuildExplorerButton.interactable = false;
		}
	}

	public void CloseClick() {
		if(Menu != null) {
			Menu.SetActive(false);
		}
	}

	public void BuildFighterClick() {
		GameState gameState = GameState.Instance;
		PlayerState playerState = PlayerState.Instance;
		SpaceHex hex = gameState.SelectedConstructionStation.transform.parent.GetComponent<SpaceHex>();
		hex.SpawnFighterSquadron();
		playerState.Materials -= 600;
	}

	public void BuildExplorerClick() {
		GameState gameState = GameState.Instance;
		PlayerState playerState = PlayerState.Instance;
		SpaceHex hex = gameState.SelectedConstructionStation.transform.parent.GetComponent<SpaceHex>();
		hex.SpawnExplorerSquadron();
		playerState.Materials -= 500;
	}
}
