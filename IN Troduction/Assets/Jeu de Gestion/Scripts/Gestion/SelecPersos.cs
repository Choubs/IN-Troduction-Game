using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelecPersos : MonoBehaviour {

	public ChargerPersos  persos;
	Perso[] TabPersos;
	Perso[] TabPersosChoisi;
	int nbPersosMax = 6;

	
	void Start () {
		
	}


	void Update () {
	
	}

	public Perso[] selectionRNG(){

		TabPersos = persos.GetTabPerso();
		List<int> tirage = new List<int>();
		TabPersosChoisi = new Perso[nbPersosMax];
		bool ok=false;
		for( int i=0; i<nbPersosMax; i++ ){
			do{
				ok=false;
				int j = Random.Range(0,persos.GetNbPerso()-1);

				if(!tirage.Contains(j)){
					tirage.Add(j);
					TabPersosChoisi[i] = TabPersos[j];
					ok = true;
				}
			}while(!ok);
			print ("Perso choisi : perso num " + TabPersosChoisi[i].name + " --> " + i);
		}

		
		return TabPersosChoisi;
	}
}
