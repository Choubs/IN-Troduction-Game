using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {

	public ChargerPersos  persos;
	public ChargerProjets  projets;
	public ChargerEnigmes  enigmes;
	public ChargerPostes  postes;

	public SelecPersos choixPerso;
	public SelecProjets choixProjet;

	static int nbPerso = 6;
	static int nbPersoSelec = 5;
	static int nbProjet = 3;
	static int nbPoste = 4;


	Perso[] TabPersos = new Perso[nbPerso];
	Perso[] TabPersosJeu = new Perso[nbPersoSelec];
	Projet[] TabProjets = new Projet[nbProjet];
	Poste[] TabPostes = new Poste[nbPoste];

	bool ProjetIsSelected = false;
	bool PersoIsSelected = false;
	bool game = false;

	int curProject = 0;
	int deletedPerso = 0;

	GameObject[] persoPrefab = new GameObject[nbPersoSelec];
	int[] evolution = new int[nbPersoSelec];

	//xp du projet en cours
	public float xpProjetJeu1 = 0.0f;
	public float xpProjetJeu2 = 0.0f;
	public float xpProjetJeu3 = 0.0f;
	public float xpProjetJeu4 = 0.0f;

	bool Projet1Ended = false;
	bool Projet2Ended = false;
	bool Projet3Ended = false;
	bool Projet4Ended = false;

	private float nextActionTime = 0.0f;
	private float period = 0.1f;


	void Start () {
		persos.Load();
		projets.Load();
		enigmes.Load ();

		//chargement des postes
		TabPostes = postes.GetTabPoste();

		//Selection aléatoire des 6 Persos
		TabPersos = choixPerso.selectionRNG();

		//Tirage au sort de 3 projets
		TabProjets = choixProjet.selectionRNG();


	}

	void Update () {
		if(!game){ //début de partie

			curProject = 1; //INTERFACE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			ProjetIsSelected = true;

			if(ProjetIsSelected){
				
				deletedPerso = 2;
				PersoIsSelected = true; //INTERFACE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

				if(PersoIsSelected){

					for( int i=0; i<nbPersoSelec ; i++){
						if(i>=deletedPerso)
							TabPersosJeu[i] = TabPersos[i+1];
						else
							TabPersosJeu[i] = TabPersos[i];
						print(TabPersosJeu[i].name);
						evolution[i] = 1000;
						persoPrefab[i] = Instantiate(Resources.Load("Productions3D/Personnage/FBX/IN_Troduction_perso")) as GameObject;
						
					}						
					game = true;
					//lancement de la partie
				}
			}

		}
		else{ //une fois le jeu lancer

			//menu pour choisir poste/formation de chaque persos
			if (Time.time > nextActionTime ) {
				nextActionTime += period;
				tick ();
			}




			//fin du jeu
			if(Projet1Ended && Projet2Ended && Projet3Ended && Projet4Ended)
				End();
			//lancement aléatoire d'énigme INTERFACE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

			//gain de points semi-aléatoire

			//sauvegarde données 
		}
	}
	void tick(){

		for(int i=0 ; i<nbPersoSelec ; i++){
			if(TabPersosJeu[i].poste == 0){
				TabPersosJeu[i].poste = Random.Range(1,5);  //INTERFACE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
				print(TabPersosJeu[i].poste.ToString()+ " poste " + TabPersosJeu[i].name);
			}

			TabPersosJeu[i].xp += Random.Range(1,10);

			if(TabPersosJeu[i].xp >= evolution[i]){
				TabPersosJeu[i].levelXp += 1;
				TabPersosJeu[i].xp = 0;
				evolution[i] += Random.Range(70,100);
				switch(TabPersosJeu[i].poste){
				case 1:
					TabPersosJeu[i].xp1 = Random.Range(4,6);
					break;
				case 2:
					TabPersosJeu[i].xp2 = Random.Range(4,6);
					break;
				case 3:
					TabPersosJeu[i].xp3 = Random.Range(4,6);
					break;
				case 4:
					TabPersosJeu[i].xp4 = Random.Range(4,6);
					break;
				}
				print(TabPersosJeu[i].name + " " + TabPersosJeu[i].levelXp);
			}

			switch(TabPersosJeu[i].poste){
			case 1:
				if(!Projet1Ended){
					xpProjetJeu1 += ((float)TabPersosJeu[i].xp1) / 1500.0f + Random.Range(0.0f, 0.3f);
					if(xpProjetJeu1 > TabProjets[curProject].xpPoste1) Projet1Ended = true;
				} break;
			case 2:
				if(!Projet2Ended){
					xpProjetJeu2 += ((float)TabPersosJeu[i].xp2) / 1500.0f + Random.Range(0.0f, 0.3f); 
					if(xpProjetJeu2 > TabProjets[curProject].xpPoste2) Projet2Ended = true; 
				} break;
			case 3:
				if(!Projet3Ended){
					xpProjetJeu3 += ((float)TabPersosJeu[i].xp3) / 1500.0f + Random.Range(0.0f, 0.3f);
					if(xpProjetJeu3 > TabProjets[curProject].xpPoste3) Projet3Ended = true; 
				} break;
			case 4:
				if(!Projet4Ended){
					xpProjetJeu4 += ((float)TabPersosJeu[i].xp4) / 1500.0f + Random.Range(0.0f, 0.3f); 
					if(xpProjetJeu4 > TabProjets[curProject].xpPoste4) Projet4Ended = true;
				} break;
			}
		}
	}

	void End(){
		//INTERFACE FIN !!!!!!!!!!!
	}
	
	void OnGUI () {
		
		GUI.Box(new Rect(50,40,100,20), xpProjetJeu1.ToString("0") + " Poste1");
		GUI.Box(new Rect(50,60,100,20), xpProjetJeu2.ToString("0") + " Poste2");
		GUI.Box(new Rect(50,80,100,20), xpProjetJeu3.ToString("0") + " Poste3");
		GUI.Box(new Rect(50,100,100,20), xpProjetJeu4.ToString("0") + " Poste4");
		// Make a background box
		/*if(!ProjetIsSelected)
			GUI.Box(new Rect(5,5,Screen.width - 5,Screen.height - 5), "Choisie ton projet");*/
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		/*if(GUI.Button(new Rect(50,40,80,20), "Level 1")) {
			Application.LoadLevel(1);
		}*/
	}

}
