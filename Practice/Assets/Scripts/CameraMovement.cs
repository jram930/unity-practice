using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float CameraMoveSpeed = 100f;
	public float ZoomSensitivity = 10f;
	public float MaxFov = 50f;
	public float MinFov = 5f;
	private Camera camera;

	// Use this for initialization
	void Start () {
		this.camera = this.gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		this.HandlePanning();
		this.HandleZooming();
	}

	private void HandlePanning() {
		float inputHorizontal = Input.GetAxis("Horizontal");
		float inputVertical = Input.GetAxis("Vertical");
		float moveHorizontal = 0f;
		float moveVertical = 0f;
		float moveHeight = 0f;
		if (inputHorizontal > 0f) {
			moveHorizontal += 1f;
		}
		if (inputHorizontal < 0f) {
			moveHorizontal -= 1f;
		}
		if (inputVertical > 0f) {
			moveVertical += 1f;
		}
		if (inputVertical < 0f) {
			moveVertical -= 1f;
		}
		Vector3 moveVector = new Vector3(moveHorizontal, moveHeight, moveVertical) * Time.deltaTime * CameraMoveSpeed;
		this.transform.Translate(moveVector, Space.World);
	}

	private void HandleZooming() {
		float fov = this.camera.fieldOfView;
		fov -= Input.GetAxis("Mouse ScrollWheel") * ZoomSensitivity;
		fov = Mathf.Clamp(fov, MinFov, MaxFov);
		this.camera.fieldOfView = fov;
	}
}
