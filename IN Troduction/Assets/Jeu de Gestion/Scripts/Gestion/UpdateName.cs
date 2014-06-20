using UnityEngine;
using System.Collections;

public class UpdateName : MonoBehaviour {

	GameObject main;

	// Use this for initialization
	void Start () {
		main = GameObject.Find("Main Camera") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh>().text = main.GetComponent<MainGame>().GetName((GetComponentInParent<onClick>().perso));
		if(main.GetComponent<MainGame>().GetFormation(GetComponentInParent<onClick>().perso)) GetComponent<TextMesh>().color = Color.gray;
		else GetComponent<TextMesh>().color = Color.white;

	}
}
