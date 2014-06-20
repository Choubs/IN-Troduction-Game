using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	public GameObject cam;
	Vector3 position;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		position = cam.transform.position;
		position.y = transform.position.y;
		transform.LookAt(position);
	}
}
