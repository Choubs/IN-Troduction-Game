using UnityEngine;
using System.Collections;

public class StartNetwork : MonoBehaviour {

	public bool server;
	public int listenPort = 25000; //Le port d'écoute du serveur
	public string remoteIP; //l'adresse IP du serveur auquel les client se connecteront
	
	private GameObject spawn;
	public GameObject player;
	public GameObject playerInst;

	
	void Awake()
	{
		DontDestroyOnLoad(this);
	}
	
	void Start () 
	{
		
		if(server)
		{
			Network.InitializeServer(32, listenPort, false); //le false signifie qu'on utilise pas le Nat punchtrough. Je vous recommande la doc d'Unity pour en savoir plus
			
			// On préviens tous nos objets que le réseau est lancé
			foreach (Object go in FindObjectsOfType(typeof(GameObject)))
				this.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			Network.Connect(remoteIP, listenPort);
		}
		
		Application.LoadLevel("Menu");

		
	}
	
	void OnPlayerConnected(NetworkPlayer player)
	{
		if(server)
		{
			print("Connecté !");
		}
	}
	
	public IEnumerator OnLevelWasLoaded()
	{
		if (Application.loadedLevelName == "Connexion") {
						Destroy (this.gameObject);
						
				}
		//if (Application.loadedLevelName == "Menu")
			//Destroy (this.gameObject);
		// Notify our objects that the level and the network are ready
		foreach (Object go in FindObjectsOfType(typeof(GameObject)))
			this.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		
		spawn = GameObject.FindGameObjectWithTag("Spawn");
		
		if(!server)
			yield return new WaitForSeconds(3);
		
		playerInst = Network.Instantiate(player, spawn.transform.position, Quaternion.identity, 0) as GameObject;
		//playerInst.transform.eulerAngles = Vector3(0, 0, 90);
		//choixInst = Network.Instantiate (choix, spawnNiveau.transform.position, Quaternion.identity, 0) as GameObject;

		//GameObject.FindGameObjectWithTag ("PorteNiveau").GetComponent<ChoixNiveau> ().playerInstNiveau = playerInst;
	}


	void Update () {

		
	}
}