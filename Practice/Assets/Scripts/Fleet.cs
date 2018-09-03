using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SphereCollider>().material = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.parent = this.transform;
		sphere.transform.position = new Vector3(this.transform.position.x + 0.8f, this.transform.position.y + 2f, this.transform.position.z + 0.3f);
		sphere.transform.localScale = new Vector3(10,10,10);
		Material selectedUnitMaterial = Resources.Load<Material>("materials/SelectedUnit") as Material;
		sphere.GetComponent<MeshRenderer>().material = selectedUnitMaterial;
	}
}
