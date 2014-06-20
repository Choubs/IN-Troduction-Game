using UnityEngine;
using System.Collections;


public class CCTVPlayerDetection : MonoBehaviour
{
	public GameObject player;
	Vector3 resetCube;
	bool lacheCube = false;
	public GameObject Cube;

	
	
	void Start ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Network").GetComponent<StartNetwork> ().playerInst;

	}

	
	
	void OnTriggerEnter (Collider other)
	{
		// If the colliding gameobject is the player...
		if(other.gameObject == player)
		{
			// ... raycast from the camera towards the player.
			Vector3 relPlayerPos = player.transform.position - transform.position;
			RaycastHit hit;
			
			if(Physics.Raycast(transform.position, relPlayerPos, out hit))
				// If the raycast hits the player...
				if(hit.collider.gameObject == player && Cube.GetComponent<PorterPC>().getPorter())
				{	
					Debug.Log("Collision ! Collision!");
					
					Cube.GetComponent<PorterPC>().setPorter(lacheCube);
					resetCube = Cube.GetComponent<PorterPC> ().getBasePosition ();
					GameObject.FindGameObjectWithTag("ObjectCube").transform.position = resetCube;
				}

		}
	}
}
