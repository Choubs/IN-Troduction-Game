using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject networkMaster; // Prefab
	
	private GameObject instantiatedMaster; //Prefab instancié
	private StartNetwork scriptStartNet;
	
	private string serverIP = "127.0.0.1";
	private int serverPort = 25000;
	
	void OnGUI()
	{
		int menuSizeX = 460;
		int menuSizeY = 115;
		float menuPosX = 20;
		float menuPosY = Screen.height/2 - menuSizeY/2;
		int sizeButtonX = 250;
		int sizeButtonY = 30;
		
		//Le menu de base
		GUI.BeginGroup(new Rect(20, Screen.height/2 - 115/2, 460, 115), "Connexion");
		GUI.Box(new Rect(0,0,menuSizeX, menuSizeY), "");

		
		//La demande de champs d'ip pour rejoindre un serveur
		serverIP = GUI.TextField(new Rect(sizeButtonX + 30, 60, 120, 30), serverIP, 40);
		
		if ( GUI.Button(new Rect(10, 20, sizeButtonX, sizeButtonY), "Créer serveur"))
		{
			//Création du serveur
			instantiatedMaster = Instantiate(networkMaster, Vector3.zero, Quaternion.identity) as GameObject;
			scriptStartNet = instantiatedMaster.GetComponent<StartNetwork>();
			scriptStartNet.server = true;
			scriptStartNet.listenPort = serverPort;
		}
		if ( GUI.Button(new Rect(10, 60, sizeButtonX, sizeButtonY), "Rejoindre serveur"))
		{
			//Rejoindre serveur
			instantiatedMaster = Instantiate(networkMaster, Vector3.zero, Quaternion.identity) as GameObject;
			scriptStartNet = instantiatedMaster.GetComponent<StartNetwork>();
			scriptStartNet.server = false;
			scriptStartNet.remoteIP = serverIP;
			scriptStartNet.listenPort = serverPort;
		}
		GUI.EndGroup();
	}
}