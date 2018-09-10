using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedObjectPanel : MonoBehaviour {

	private Text selectedObjectName;
	private GameObject selectedObjectPanel;

	void Start() {
		this.selectedObjectPanel = this.transform.Find("SelectedObjectPanel").gameObject;
		this.selectedObjectName = GameObject.Find("SelectedObjectName").GetComponent<Text>();
		this.selectedObjectPanel.SetActive(false);
	}
	
	void Update () {
		GameState gameState = GameState.Instance;
		if (gameState.SelectedUnit != null) {
			this.selectedObjectPanel.SetActive(true);
			this.selectedObjectName.text = gameState.SelectedUnit.name;
		}
		else {
			this.selectedObjectPanel.SetActive(false);
		}
	}
}
