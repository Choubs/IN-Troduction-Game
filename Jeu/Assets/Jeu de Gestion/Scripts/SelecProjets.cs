using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelecProjets : MonoBehaviour {

	public ChargerProjets  projets;
	Projet[] TabProjets;
	Projet[] TabProjetsChoisi;
	int nbProjetsMax = 3;

	
	void Start () {
		
	}


	void Update () {
	
	}

	public Projet[] selectionRNG(){

		TabProjets = projets.GetTabProjet();
		List<int> tirage = new List<int>();
		TabProjetsChoisi = new Projet[nbProjetsMax];
		bool ok = false;
		for( int i=0; i<nbProjetsMax; i++ ){
			do{
				ok=false;
				int j = Random.Range(0,projets.GetNbProjet()-1);

				if(!tirage.Contains(j) && TabProjets[j].typeProjet == i+1){
					tirage.Add(j);
					TabProjetsChoisi[i] = TabProjets[j];
					ok = true;
				}
			}while(!ok);
			print ("Projet choisi : " + TabProjetsChoisi[i].name + " --> " + i);
		}

		
		return TabProjetsChoisi;
	}
}
