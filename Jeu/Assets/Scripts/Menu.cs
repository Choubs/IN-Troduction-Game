using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject comfirm;
	public GameObject annuler;
	public GameObject av1;
	public GameObject av2;
	public GameObject av3;
	public GameObject av4;
	public GameObject perso;
	private Texture2D[] text;
	private Texture2D tempText;
	private int nbImage = 4;

	// Use this for initialization
	void Start (){
		tempText = Resources.Load("Picture/defaut") as Texture2D;
		UIEventListener.Get(comfirm).onClick = Ok;
		UIEventListener.Get(annuler).onClick = Annuler;
		UIEventListener.Get(av1).onClick = Avatar1;
		UIEventListener.Get(av2).onClick = Avatar2;
		UIEventListener.Get(av3).onClick = Avatar3;
		UIEventListener.Get(av4).onClick = Avatar4;
		text = new Texture2D[nbImage];
		for(int i = 0; i<nbImage; i++)
		{
			text[i] = Resources.Load("Pictures/" +(i+1).ToString()) as Texture2D;
		}
	}

	void Ok (GameObject go) {
		perso.renderer.material.mainTexture = tempText;
		this.gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Annuler (GameObject go) {
		this.gameObject.SetActive(false);
	}

	void Avatar1 (GameObject go) {
		 tempText= text[0];
	}

	void Avatar2 (GameObject go) {
		tempText = text[1];
	}

	void Avatar3 (GameObject go) {
		tempText = text[2];
	}

	void Avatar4 (GameObject go) {
		tempText = text[3];
	}
}