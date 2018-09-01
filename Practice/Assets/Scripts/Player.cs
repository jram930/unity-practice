using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float MoveMultiplier = 6f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		transform.Translate(new Vector3(moveHorizontal, 0f, moveVertical) * Time.deltaTime * MoveMultiplier);
	}
}
