using UnityEngine;
using System.Collections;

public class ClickProjet : MonoBehaviour {

	public int choix;
	public MainGame game;
	public GameObject MenuToClose;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		game.setProjet(choix);
		MenuToClose.SetActive(false);
	}
}
