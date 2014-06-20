using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	
	public GameObject networkMaster; // Prefab
	
	private GameObject instantiatedMaster; //Prefab instancié
	private StartNet scriptStartNet;
	
	public string serverIP = "127.0.0.1";
	private int serverPort = 25000;
	
	void  OnGUI (){
		int menuSizeX = 460;
		int menuSizeY = 115;
		float menuPosX = 20;
		float menuPosY = Screen.height/2 - menuSizeY/2;
		//GUI.Box (new Rect (menuPosX,menuPosY,menuSizeX,menuSizeY), "Main Menu");
		int sizeButtonX = 250;
		int sizeButtonY = 30;
		
		//Le menu de base
		//GUI.Box( new Rect(0,0,menuSizeX, menuSizeY), "");
		
		//La demande de champs d'ip pour rejoindre un serveur
		serverIP = GUI.TextField(new Rect(sizeButtonX - 50, 230, 120, 30), serverIP, 40);
		
		//GUI.EndGroup();
	}
	
	public void  CreerServeur (){
		//Création du serveur
		instantiatedMaster = Instantiate(networkMaster, Vector3.zero, Quaternion.identity) as GameObject;
		scriptStartNet = instantiatedMaster.GetComponent<StartNet>();
		scriptStartNet.server = true;
		scriptStartNet.listenPort = serverPort;
	}
	
	public void  RejoindreServeur (){
		//Rejoindre serveur
		instantiatedMaster = Instantiate(networkMaster, Vector3.zero, Quaternion.identity) as GameObject;
		scriptStartNet = instantiatedMaster.GetComponent<StartNet>();
		scriptStartNet.server = false;
		scriptStartNet.remoteIP = serverIP;
		scriptStartNet.listenPort = serverPort;
	}
}