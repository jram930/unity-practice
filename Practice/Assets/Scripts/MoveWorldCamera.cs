using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorldCamera : MonoBehaviour {

	public float CameraMoveSpeed = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float inputHorizontal = Input.GetAxis("Horizontal");
		float inputVertical = Input.GetAxis("Vertical");
		float moveHorizontal = 0f;
		float moveVertical = 0f;
		float moveHeight = 0f;
		if(inputHorizontal > 0f) {
			moveHorizontal += 1f;
			moveVertical += .6f;
		}
		if(inputHorizontal < 0f) {
			moveHorizontal -= 1f;
			moveVertical -= .6f;
		}
		if(inputVertical > 0f) {
			moveHorizontal -= .6f;
			moveVertical += 1f;
		}
		if(inputVertical < 0f) {
			moveHorizontal += .6f;
			moveVertical -= 1f;
		}
		Vector3 moveVector = new Vector3(moveHorizontal, moveHeight, moveVertical) * Time.deltaTime * CameraMoveSpeed;
		this.transform.Translate(moveVector, Space.World);
	}
}
