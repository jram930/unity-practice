using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject ObjectToFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float posX = ObjectToFollow.transform.position.x;
		float posY = ObjectToFollow.transform.position.y + 3f;
		float posZ = ObjectToFollow.transform.position.z - 8f;
		this.transform.position = new Vector3(posX, posY, posZ);
	}
}
