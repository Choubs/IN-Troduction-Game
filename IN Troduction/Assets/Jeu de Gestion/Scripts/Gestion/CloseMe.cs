using UnityEngine;
using System.Collections;

public class CloseMe : MonoBehaviour {

	public MainGame game;
	public GameObject MenuToClose;
	
	void OnClick(){
		MenuToClose.SetActive(false);
	}
}
