using UnityEngine;
using System.Collections;

public class Porter : TouchLogic {

	public GameObject Pos;
	public Vector3 resetPosition;
	public GameObject player;
	public bool porte = false;
	

	// Use this for initialization
	void Start () 
	{
		resetPosition = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);

	}
    
	void OnTouchBegan3D()
	{
		if (porte) 
		{
			porte = false;
			GameObject.FindGameObjectWithTag ("DetectionCollider").GetComponent<CCTVPlayerDetection> ().Cube = null;
		}
		if (!porte && Mathf.Abs(player.transform.position.y) - Mathf.Abs (this.transform.position.y) < 0.5f && 
		               Mathf.Abs(player.transform.position.x) - Mathf.Abs(this.transform.position.x) < 0.5f /*&&
		    			Mathf.Abs(player.transform.position.z) - Mathf.Abs(this.transform.position.z) < 0.5f*/)
			porte = true;
	}

	void Update()
	{
		if (porte)
		{
			this.transform.position = new Vector3 (Pos.transform.position.x,
				                                   Pos.transform.position.y,
				                                   Pos.transform.position.z);
			GameObject.FindGameObjectWithTag ("DetectionCollider").GetComponent<CCTVPlayerDetection> ().Cube = this.gameObject;
		}

	}

	public Vector3 getBasePosition ()
	{
		return resetPosition;
	}

	public void setPorter(bool tempPorte)
	{
		porte = tempPorte;
	}
}
