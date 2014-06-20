using UnityEngine;
using System.Collections;

public class PostesEnd : MonoBehaviour {

	public MainGame game;
	public GameObject MenuToClose;
	
	
	void OnClick(){
		MenuToClose.SetActive(false);
		game.SetPosteSelected();
	}
}
