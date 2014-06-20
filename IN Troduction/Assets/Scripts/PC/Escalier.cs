using UnityEngine;
using System.Collections;

public class Escalier : MonoBehaviour {

	public GameObject cubeEsc;
	bool AfficherTouche = false;
	public GameObject player;
	public GameObject[] tabEsc;
	public int cpt, cptTemp = 0;
	// Use this for initialization
	void Start () {
		tabEsc = GameObject.FindGameObjectsWithTag("Marche");
		for (int i = 0; i < tabEsc.Length; i++)
			tabEsc [i].active = false;
		cpt = tabEsc.Length;
		player = GameObject.FindGameObjectWithTag ("Network").GetComponent<StartNetwork> ().playerInst;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("b") && AfficherTouche && cubeEsc.GetComponent<PorterPC> ().getPorter ()) {
		
			if (cptTemp < cpt)
			{
				tabEsc[cptTemp].active = true;
				Destroy(cubeEsc);
				cubeEsc = null;
				cptTemp++;
			}

		}

	
	}

	void OnTriggerEnter ()
	{
		if (cubeEsc.GetComponent<PorterPC> ().getPorter ())
			AfficherTouche = true;
	}
	
	void OnTriggerExit(){
		AfficherTouche = false;
	}
	
	void OnGUI(){
		if(AfficherTouche) 
			GUI.Box (new Rect (Screen.width/2,Screen.height-Screen.height+50,110,20), "Appuyer sur le clic gauche");
	}
}
