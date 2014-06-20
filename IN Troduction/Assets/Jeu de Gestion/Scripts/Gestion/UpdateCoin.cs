using UnityEngine;
using System.Collections;

public class UpdateCoin : MonoBehaviour {
	
	GameObject main;

	void Start () {
		main = GameObject.Find("Main Camera") as GameObject;
	}

	void Update () {
		GetComponent<UILabel>().text = main.GetComponent<MainGame>().credit.ToString() + " crédits";
	}
}
