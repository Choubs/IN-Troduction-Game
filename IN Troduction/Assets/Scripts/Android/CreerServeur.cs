using UnityEngine;
using System.Collections;

public class CreerServeur : TouchLogic {
	
	public Menu createServer; 

	void Awake()
	{
		//creer = this.GetComponent<MainMenu>;
	}

	public void OnTouchBegan()
	{
		createServer.CreerServeur();
	}
}
