using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float CameraMoveSpeed = 100f;
	public float ZoomSensitivity = 10f;
	public float MaxFov = 50f;
	public float MinFov = 5f;
	private Camera cam;

	// Use this for initialization
	void Start () {
		this.cam = this.gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		this.FocusOnObject();
		this.HandlePanning();
		this.HandleRaising();
		this.HandleRotating();
	}

	private void FocusOnObject() {
		GameState gameState = GameState.Instance;
		float inputCameraFocus = Input.GetAxis("CameraFocus");
		if(inputCameraFocus > 0 && gameState.SelectedUnit != null) {
			this.cam.transform.LookAt(gameState.SelectedUnit.transform);
			gameState.CameraFocusPoint.transform.parent = gameState.SelectedUnit.transform;
			gameState.CameraFocusPoint.transform.position = gameState.SelectedUnit.transform.position;
		}
	}

	private void HandleRotating() {
		float inputCameraRotate = Input.GetAxis("CameraRotate");
		float inputMouseX = Input.GetAxis("MouseX");
		float inputMouseY = Input.GetAxis("MouseY");
		if(inputCameraRotate > 0) {
			float yRotateSpeed = inputMouseX * 5f;
			float xRotateSpeed = inputMouseY * 5f;
			GameState gamestate = GameState.Instance;
			GameObject focusPoint = gamestate.CameraFocusPoint;
			Vector3 angles = focusPoint.transform.eulerAngles;
			float rotationX = angles.x - xRotateSpeed;
			float rotationY = angles.y + yRotateSpeed;
			//Quaternion fromRotation = Quaternion.Euler(focusPoint.transform.rotation.eulerAngles.x, focusPoint.transform.rotation.eulerAngles.y, 0f);
			Quaternion toRotation = Quaternion.Euler(rotationX, rotationY, 0f);
			focusPoint.transform.rotation = toRotation;
		}
	}

	private void HandlePanning() {
		GameState gameState = GameState.Instance;
		float inputHorizontal = Input.GetAxis("Horizontal");
		float inputVertical = Input.GetAxis("Vertical");
		float moveHorizontal = 0f;
		float moveVertical = 0f;
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
		Vector3 moveVector = new Vector3(moveHorizontal, 0f, moveVertical) * Time.deltaTime * CameraMoveSpeed;
		if (gameState.CameraFocusPoint != null) {
			gameState.CameraFocusPoint.transform.Translate(moveVector, Space.Self);
		}
	}

	private void HandleRaising() {
		float scrollInput = Input.GetAxis("Mouse ScrollWheel");
		if(scrollInput != 0) {
			GameState gameState = GameState.Instance;
			float move = 0f;
			if(scrollInput < 0) {
				move = -1f;
			} else {
				move = 1f;
			}
			Vector3 moveVector = new Vector3(0f, move, 0f) * Time.deltaTime * CameraMoveSpeed * 50f;
			gameState.CameraFocusPoint.transform.Translate(moveVector, Space.Self);
		}
	}
}
