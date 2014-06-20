using UnityEngine;
using System.Collections;

public class ClickPerso : MonoBehaviour {

	public int choix;
	public MainGame game;
	public GameObject MenuToClose;


	void OnClick(){
		game.setPerso(choix);
		MenuToClose.SetActive(false);
	}
}
