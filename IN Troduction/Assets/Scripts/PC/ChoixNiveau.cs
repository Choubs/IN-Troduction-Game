using UnityEngine;
using System.Collections;

public class ChoixNiveau : MonoBehaviour {

	bool AfficherTouche = false;
	public GameObject player, playerInstNiveau;
	private GameObject spawn;
	bool server;
	// Use this for initialization
	void Start () {
		spawn = GameObject.FindGameObjectWithTag("Spawn");
		//playerInstNiveau = GameObject.FindGameObjectWithTag("Network").GetComponent<StartNetwork>().playerInst;
		server = GameObject.FindGameObjectWithTag ("Network").GetComponent<StartNetwork> ().server;

	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown ("b") && AfficherTouche) {
		
		//}
		//if (Input.GetKeyDown ("b")) {

		//}
	
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.transform.root.networkView.isMine) 
			AfficherTouche = true;
	}
	
	void OnTriggerExit(){
		AfficherTouche = false;
	}
	
	void OnGUI(){
		if (AfficherTouche) {
			//GUI.Box (new Rect (Screen.width / 2, Screen.height - Screen.height + 50, 110, 20), "Appuyer sur b");

			GUI.BeginGroup (new Rect(20, Screen.height/2 - 115/2, 460, 115), "");
			GUI.Box (new Rect (0, 0, 460, 115), "Choix du Niveau");

			if ( GUI.Button(new Rect(10, 20, 250, 30), "Jeu de Coopératon"))
			{
				Application.LoadLevel("Coop");
			}
			if ( GUI.Button(new Rect(10, 60, 250, 30), "Jeu de Gestion"))
			{
				Application.LoadLevel("JeuDeGestion");
			}
			GUI.EndGroup();

		}
	}

	/*IEnumerator OnLevelWasLoaded()
	{
		if(!server)
			yield return new WaitForSeconds(3);

		Destroy (GameObject.FindGameObjectWithTag("Player"));
		playerInstNiveau = Network.Instantiate(player, spawn.transform.position, Quaternion.identity, 0) as GameObject;
		

	}*/

}
