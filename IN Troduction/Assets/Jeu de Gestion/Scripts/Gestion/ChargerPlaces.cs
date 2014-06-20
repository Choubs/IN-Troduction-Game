using UnityEngine;
using System.Collections;

public class ChargerPlaces : MonoBehaviour {


	public GameObject[] places;

	public GameObject place1;
	public GameObject place2;
	public GameObject place3;
	public GameObject place4;
	public GameObject place5;

	void Start(){

	}

	public GameObject[] GetPlaces(){
		places = new GameObject[5];
		places[0] = place1;
		places[1] = place2;
		places[2] = place3;
		places[3] = place4;
		places[4] = place5;
		return places;
	}
}
