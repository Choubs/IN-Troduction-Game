using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {

	public ChargerPersos  persos;
	public ChargerProjets  projets;
	public ChargerEnigmes  enigmes;
	public ChargerPostes  postes;
	public ChargerPlaces  places;

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
	bool PosteIsSelected = false;
	bool game = false;

	int curProject = 0;
	int deletedPerso = 0;

	GameObject[] persoPrefab = new GameObject[nbPersoSelec];
	GameObject[] TabPlaces = new GameObject[nbPersoSelec];
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
	private float projetTime = 0.0f;
	
	public GameObject SelectionPerso;
	public GameObject SelectionPoste;

	public ChargerLabelMenu menu;

	public UISlider sliderPoste1;
	public UISlider sliderPoste2;
	public UISlider sliderPoste3;
	public UISlider sliderPoste4;

	bool created = false;

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



		updateText();
	}

	void updateText(){

		sliderPoste1.sliderValue = 0.0f;
		sliderPoste2.sliderValue = 0.0f;
		sliderPoste3.sliderValue = 0.0f;
		sliderPoste4.sliderValue = 0.0f;


		menu.DescMovie.text = TabProjets[0].description;
		menu.DescGame.text = TabProjets[1].description;
		menu.DescSoft.text = TabProjets[2].description;

		menu.NameMovie.text = TabProjets[0].name;
		menu.NameGame.text = TabProjets[1].name;
		menu.NameSoft.text = TabProjets[2].name;

		menu.CV1.text = TabPersos[0].cv;
		menu.CV2.text = TabPersos[1].cv;
		menu.CV3.text = TabPersos[2].cv;
		menu.CV4.text = TabPersos[3].cv;
		menu.CV5.text = TabPersos[4].cv;
		menu.CV6.text = TabPersos[5].cv;

		menu.Name1.text = TabPersos[0].name;
		menu.Name2.text = TabPersos[1].name;
		menu.Name3.text = TabPersos[2].name;
		menu.Name4.text = TabPersos[3].name;
		menu.Name5.text = TabPersos[4].name;
		menu.Name6.text = TabPersos[5].name;
	}

	void Update () {
		if(!game){ //début de partie
			if(ProjetIsSelected){
				if(PersoIsSelected){
					if(!created){ 
						deletePerso();
						created = true;
					}
					if(PosteIsSelected){
						init();					
						game = true;
						//lancement de la partie
					}
				}
			}
		}
		else{ //une fois le jeu lancer
			
			menu.Chrono.text =  (Time.time - projetTime).ToString("0.00") + " / "  + TabProjets[curProject].tempsRequis.ToString(); 

			//menu pour choisir poste/formation de chaque persos
			if (Time.time > nextActionTime ) { //Toute les 0.1 secondes 
				nextActionTime += period;
				tick ();
			}
			//fin du jeu
			if( (Time.time - projetTime) > TabProjets[curProject].tempsRequis) End(false);
			if(Projet1Ended && Projet2Ended && Projet3Ended && Projet4Ended )
				End(true);


			//lancement aléatoire d'énigme INTERFACE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//gain de points semi-aléatoire
			//sauvegarde données 
		}
	}


	void init(){
		TabPlaces = places.GetPlaces();
		for( int i=0; i<nbPersoSelec ; i++){
			evolution[i] = 1000;
			persoPrefab[i] = Instantiate(Resources.Load("Productions3D/Personnage/FBX/IN_Troduction_perso")) as GameObject;

			persoPrefab[i].transform.position = TabPlaces[i].transform.position;
		}
	}

	void deletePerso(){
		for( int i=0; i<nbPersoSelec ; i++){
			if(i>=deletedPerso)
				TabPersosJeu[i] = TabPersos[i+1];
			else
				TabPersosJeu[i] = TabPersos[i];
			print(TabPersosJeu[i].name);
		}
		
		
		menu.CV1Poste.text = TabPersosJeu[0].cv;
		menu.CV2Poste.text = TabPersosJeu[1].cv;
		menu.CV3Poste.text = TabPersosJeu[2].cv;
		menu.CV4Poste.text = TabPersosJeu[3].cv;
		menu.CV5Poste.text = TabPersosJeu[4].cv;
		
		menu.Name1Poste.text = TabPersosJeu[0].name;
		menu.Name2Poste.text = TabPersosJeu[1].name;
		menu.Name3Poste.text = TabPersosJeu[2].name;
		menu.Name4Poste.text = TabPersosJeu[3].name;
		menu.Name5Poste.text = TabPersosJeu[4].name;
	}




	void OnSelectionChange0 (string val)
	{
		switch (val)
		{
		case "poste 1":	TabPersosJeu[0].poste = 1;	break;
		case "poste 2":	TabPersosJeu[0].poste = 2;	break;
		case "poste 3":	TabPersosJeu[0].poste = 3;	break;
		case "poste 4":	TabPersosJeu[0].poste = 4;	break;
		}
		print (TabPersosJeu[0].poste.ToString());
	}
	void OnSelectionChange1 (string val)
	{
		switch (val)
		{
		case "poste 1":	TabPersosJeu[1].poste = 1;	break;
		case "poste 2":	TabPersosJeu[1].poste = 2;	break;
		case "poste 3":	TabPersosJeu[1].poste = 3;	break;
		case "poste 4":	TabPersosJeu[1].poste = 4;	break;
		}
	}
	void OnSelectionChange2 (string val)
	{
		switch (val)
		{
		case "poste 1":	TabPersosJeu[2].poste = 1;	break;
		case "poste 2":	TabPersosJeu[2].poste = 2;	break;
		case "poste 3":	TabPersosJeu[2].poste = 3;	break;
		case "poste 4":	TabPersosJeu[2].poste = 4;	break;
		}
	}
	void OnSelectionChange3 (string val)
	{
		switch (val)
		{
		case "poste 1":	TabPersosJeu[3].poste = 1;	break;
		case "poste 2":	TabPersosJeu[3].poste = 2;	break;
		case "poste 3":	TabPersosJeu[3].poste = 3;	break;
		case "poste 4":	TabPersosJeu[3].poste = 4;	break;
		}
	}
	void OnSelectionChange4 (string val)
	{
		switch (val)
		{
		case "poste 1":	TabPersosJeu[4].poste = 1;	break;
		case "poste 2":	TabPersosJeu[4].poste = 2;	break;
		case "poste 3":	TabPersosJeu[4].poste = 3;	break;
		case "poste 4":	TabPersosJeu[4].poste = 4;	break;
		}
	}



	void tick(){
		
		for(int i=0 ; i<nbPersoSelec ; i++){
			/*if(TabPersosJeu[i].poste == 0){
				TabPersosJeu[i].poste = Random.Range(1,5);  //INTERFACE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
				print(TabPersosJeu[i].poste.ToString()+ " poste " + TabPersosJeu[i].name);
			}*/

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
			float percentTemp = xpProjetJeu1 / (float)TabProjets[curProject].xpPoste1;
			sliderPoste1.sliderValue = percentTemp;

			percentTemp = xpProjetJeu2 / (float)TabProjets[curProject].xpPoste2;
			sliderPoste2.sliderValue = percentTemp;

			percentTemp = xpProjetJeu3 / (float)TabProjets[curProject].xpPoste3;
			sliderPoste3.sliderValue = percentTemp;

			percentTemp = xpProjetJeu4 / (float)TabProjets[curProject].xpPoste4;
			sliderPoste4.sliderValue = percentTemp;

		}
	}

	public void SetPosteSelected(){
		PosteIsSelected = true;	
		nextActionTime = Time.time;
		projetTime = nextActionTime;
	}

	void End( bool victoire){
		//INTERFACE FIN !!!!!!!!!!!
		if(victoire)
			print("YESSS !!!");
		else
			print("NOOOO !!!");
	}



	public void setProjet(int i){
		curProject = i;
		ProjetIsSelected = true;
		SelectionPerso.SetActive(true);
	}



	public void setPerso(int i){
		deletedPerso = i;
		PersoIsSelected = true;
		SelectionPerso.SetActive(false);
		SelectionPoste.SetActive(true);
	}



	void OnGUI () {
		
		GUI.Box(new Rect(50,40,100,20), xpProjetJeu1.ToString("0") + " Poste1");
		GUI.Box(new Rect(50,60,100,20), xpProjetJeu2.ToString("0") + " Poste2");
		GUI.Box(new Rect(50,80,100,20), xpProjetJeu3.ToString("0") + " Poste3");
		GUI.Box(new Rect(50,100,100,20), xpProjetJeu4.ToString("0") + " Poste4");
		// Make a background box
		/*if(!ProjetIsSelected)
			GUI.Box(new Rect(5,5,Screen.width - 5,Screen.height - 5), "Choisie ton projet");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		/*if(GUI.Button(new Rect(50,40,80,20), "Level 1")) {
			Application.LoadLevel(1);
		}*/
	}

}
