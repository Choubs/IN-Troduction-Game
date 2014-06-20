using UnityEngine;
using System.Collections;

public class StartNet : MonoBehaviour {
	
	
	public bool  server;
	public int listenPort = 25000; //Le port d'écoute du serveur
	public string remoteIP; //l'adresse IP du serveur auquel les client se connecteront
	
	private GameObject[] spawners;
	public GameObject cuby;
	public GameObject cubyInst;

	Joystick joyStick;
		
	void  Awake (){
		DontDestroyOnLoad(this);
	}
	
	void  Start (){
		
		if(server)
		{
			Network.InitializeServer(32, listenPort, false); //le false signifie qu'on utilise pas le Nat punchtrough. Je vous recommande la doc d'Unity pour en savoir plus
			
			// On préviens tous nos objets que le réseau est lancé
			foreach (Object go in FindObjectsOfType(typeof(GameObject)))
			{
				this.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			Network.Connect(remoteIP, listenPort);
		}

		Application.LoadLevel("Niveau1");
		
	}
	
	void  OnPlayerConnected ( NetworkPlayer player  ){
		if(server)
		{
			print("Connecté !");
		}
	}

	public IEnumerator OnLevelWasLoaded (){
		if ( Application.loadedLevelName == "Menu")
			Destroy(this.gameObject);
		// Notify our objects that the level and the network are ready
		foreach (Object go in FindObjectsOfType(typeof(GameObject)))
			this.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);

		spawners = GameObject.FindGameObjectsWithTag("Spawn");
		int rand = Random.Range(0, spawners.Length);
		GameObject spawn = spawners[rand];

		if(!server)
			yield return new WaitForSeconds(3);

		cubyInst = Network.Instantiate(cuby, spawn.transform.position, Quaternion.identity, 0) as GameObject;
		cubyInst.transform.eulerAngles =new Vector3(0, 0, 90);

		GameObject.FindGameObjectWithTag("GameMovControl").GetComponent<Joystick>().player = cubyInst.transform;
		GameObject.FindGameObjectWithTag("GameMovControl").GetComponent<Joystick>().troller = cubyInst.GetComponent<CharacterController>();

		GameObject.FindGameObjectWithTag("GameCamControl").GetComponent<Joystick>().player = cubyInst.transform;
		GameObject.FindGameObjectWithTag("GameCamControl").GetComponent<Joystick>().troller = cubyInst.GetComponent<CharacterController>();

		GameObject.FindGameObjectWithTag("ObjectCube").GetComponent<Porter>().player = cubyInst;
		GameObject.FindGameObjectWithTag("ObjectCube").GetComponent<Porter>().Pos = GameObject.FindGameObjectWithTag("Portage");

		GameObject.FindGameObjectWithTag("DetectionCollider").GetComponent<CCTVPlayerDetection>().player = cubyInst;


		
		//return cubyInst;
		
	}

	void  Update (){

		
	}

	public GameObject GetInstCuby()
	{
		return cubyInst;
	}
}