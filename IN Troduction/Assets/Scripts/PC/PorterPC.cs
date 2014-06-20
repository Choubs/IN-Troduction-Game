using UnityEngine;
using System.Collections;

public class PorterPC : MonoBehaviour {

	public GameObject Pos;
	public Vector3 resetPosition;
	//public GameObject playerPorte;
	public bool porte = false;
	

	// Use this for initialization
	void Start () 
	{
		resetPosition = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		//playerPorte = GameObject.FindGameObjectWithTag ("Network").GetComponent<StartNetwork> ().playerInst;
		Pos = GameObject.FindGameObjectWithTag ("Portage");

	}
    
	void OnTriggerStay()
	{
		print ("Collision! Collision!");
		networkView.RPC("PorterCube", RPCMode.All);
		//PorterCube ();
	}
	
	void Update()
	{
		if (porte)
			networkView.RPC("PositionCube", RPCMode.All);
						//PositionCube ();
	}
	[RPC]
	void PorterCube()
	{
			if (Input.GetButtonDown ("Fire1") && porte) {
					porte = false;
					GameObject.FindGameObjectWithTag ("DetectionCollider").GetComponent<CCTVPlayerDetection> ().Cube = null;
			}
			if (Input.GetButtonDown ("Fire1") && !porte)
					porte = true;
	}

	[RPC]
	void PositionCube()
	{
		this.transform.position = new Vector3 (Pos.transform.position.x,
                               			   Pos.transform.position.y,
                               			   Pos.transform.position.z);
		GameObject.FindGameObjectWithTag ("DetectionCollider").GetComponent<CCTVPlayerDetection> ().Cube = this.gameObject;
		GameObject.FindGameObjectWithTag ("TriggerEsc").GetComponent<Escalier> ().cubeEsc = this.gameObject;

	}
	
	public Vector3 getBasePosition ()
	{
		return resetPosition;
	}

	public void setPorter(bool tempPorte)
	{
		porte = tempPorte;
	}

	public bool getPorter()
	{
		return porte;
	}
}
