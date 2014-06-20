using UnityEngine;
using System.Collections;

public class HideMenu : MonoBehaviour {

	public GameObject panel1 ;
	void Start()
	{
		panel1.SetActive (false);

	}
	
	void OnMouseUp()
	{
		panel1.SetActive(true);
	}
}