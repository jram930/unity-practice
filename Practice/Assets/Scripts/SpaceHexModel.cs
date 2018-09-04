using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHexModel : MonoBehaviour {

	private GameObject parentHex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		this.parentHex = this.transform.parent.gameObject;
		this.parentHex.GetComponent<SpaceHex>().HandleClick();
	}
}
