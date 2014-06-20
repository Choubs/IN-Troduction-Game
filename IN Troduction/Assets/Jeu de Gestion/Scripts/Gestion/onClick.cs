using UnityEngine;
using System.Collections;

public class onClick : MonoBehaviour {

	public MainGame game ;
	public int perso;

	void Start(){
		game = GameObject.Find("Main Camera").GetComponent<MainGame>();
	}

	void Update(){
	}

	void OnMouseOver(){
		if(!game.FormationMenuOpen){
			if(!game.PosteMenuOpen){
				if(!game.pauseIsActive){
					if(Input.GetMouseButtonUp(0)){ // Left Click
						Time.timeScale = 0.0f;
						game.clickPerso = true;
						game.PosteMenuOpen = true;
					}
					if(Input.GetMouseButtonUp(1)) { //Right Click
						Time.timeScale = 0.0f;
						game.persoFormation = perso;
						game.clickFormation = true;
						game.FormationMenuOpen = true;
					}
				}
			}
		}
	}
}
		
