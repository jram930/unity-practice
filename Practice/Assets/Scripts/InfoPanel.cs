using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerState playerState = PlayerState.Instance;
		playerState.Credits = 1000;
		playerState.Materials = 1000;
		playerState.Research = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerState playerState = PlayerState.Instance;
		this.SetCreditsText(playerState.Credits);
		this.SetMaterialsText(playerState.Materials);
		this.SetResearchText(playerState.Research);
	}

	private void SetCreditsText(int credits) {
		GameObject.Find("InfoCreditsText").GetComponent<Text>().text = "Credits: " + string.Format("{0:n0}", credits);
	}

	private void SetMaterialsText(int materials) {
		GameObject.Find("InfoMaterialsText").GetComponent<Text>().text = "Materials: " + string.Format("{0:n0}", materials);
	}

	private void SetResearchText(int research) {
		GameObject.Find("InfoResearchText").GetComponent<Text>().text = "Research: " + string.Format("{0:n0}", research);
	}
}
