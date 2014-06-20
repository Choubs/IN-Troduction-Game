using UnityEngine;
using System.Collections;

public class RejoindreServeur : TouchLogic {

	public Menu joinServer; 
	
	void Awake()
	{
		//creer = this.GetComponent<MainMenu>;
	}
	
	public void OnTouchBegan()
	{
		joinServer.RejoindreServeur();
	}
}
