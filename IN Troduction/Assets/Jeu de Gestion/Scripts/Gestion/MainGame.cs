using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {

	public ChargerPersos  persos;
	public ChargerProjets  projets;
	public ChargerEnigmes  enigmes;
	public ChargerPostes  postes;
	public ChargerPlaces  places;
	public ChargerFormations  formations;
	public ChargerLabelMenu menu;

	public SelecPersos choixPerso;
	public SelecProjets choixProjet;

	public bool clickPerso;
	public bool clickFormation;
	public int persoFormation = 0;

	static int nbPerso = 6;
	static int nbPersoSelec = 5;
	static int nbProjet = 3;
	static int nbPoste = 4;


	Perso[] TabPersos = new Perso[nbPerso];
	Perso[] TabPersosJeu = new Perso[nbPersoSelec];
	Projet[] TabProjets = new Projet[nbProjet];
	Poste[] TabPostes = new Poste[nbPoste];
	Formation[] TabFormations = new Formation[nbPoste];
	Enigme[] TabEnigmes;

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
	public GameObject SelectionFormation;

	public GameObject EndVictoire;
	public GameObject EndDefaite;


	public UISlider sliderPoste1;
	public UISlider sliderPoste2;
	public UISlider sliderPoste3;
	public UISlider sliderPoste4;

	bool created = false;

	public GameObject AnnulerFormation;
	public GameObject OKFormation;
	public GameObject formation1;
	public GameObject formation2;
	public GameObject formation3;
	public GameObject formation4;
	public GameObject warning;

	int currentFormation = 0;

	public int credit = 3;

	ArrayList EnigmesUsed;
	public GameObject MenuEnigme;
	int QTemp = -1;
	public GameObject Q1;
	public GameObject Q2;
	public GameObject Q3;
	public GameObject Q4;

	public bool FormationMenuOpen = false;
	public bool PosteMenuOpen = false;


	public GameObject Pause;
	public bool pauseIsActive = false;

	public GameObject quitter;
	public GameObject recommencer;
	public GameObject continuer;


	void Start () {
		persos.Load();
		projets.Load();
		enigmes.Load ();
		formations.Load();
		postes.Load();

		TabFormations = formations.GetTabFormation();
		//chargement des postes
		TabPostes = postes.GetTabPoste();

		//Selection aléatoire des 6 Persos
		TabPersos = choixPerso.selectionRNG();

		//Tirage au sort de 3 projets
		TabProjets = choixProjet.selectionRNG();

		TabEnigmes = enigmes.GetTabEnigme();

		EnigmesUsed  = new ArrayList();

		UIEventListener.Get(AnnulerFormation).onClick = annulerFormation;
		UIEventListener.Get(OKFormation).onClick = okFormation;
		UIEventListener.Get(formation1).onClick = form1;
		UIEventListener.Get(formation2).onClick = form2;
		UIEventListener.Get(formation3).onClick = form3;
		UIEventListener.Get(formation4).onClick = form4;

		UIEventListener.Get(Q1).onClick = R1;
		UIEventListener.Get(Q2).onClick = R2;
		UIEventListener.Get(Q3).onClick = R3;
		UIEventListener.Get(Q4).onClick = R4;

		UIEventListener.Get(quitter).onClick = Quit;
		UIEventListener.Get(recommencer).onClick = Recommencer;
		UIEventListener.Get(continuer).onClick = Continuer;

		updateText();

	}

	void Quit(GameObject go){
		Application.Quit();
	}

	void Recommencer(GameObject go){
		Application.LoadLevel("JeuDeGestion");
	}

	void Continuer(GameObject go){
		Time.timeScale = 1.0f;
		pauseIsActive = false;
		Pause.SetActive(false);
	}

	void form1(GameObject go){
		currentFormation = 0;
		if(currentFormation+1 > credit){
			warning.SetActive(true);
		}
		else{
			warning.SetActive(false);
		}
	}
	void form2(GameObject go){
		currentFormation = 1;
		if(currentFormation+1 > credit){
			warning.SetActive(true);
		}
		else{
			warning.SetActive(false);
		}
	}
	void form3(GameObject go){
		currentFormation = 2;
		if(currentFormation+1 > credit){
			warning.SetActive(true);
		}
		else{
			warning.SetActive(false);
		}
	}
	void form4(GameObject go){
		currentFormation = 3;
		if(currentFormation+1 > credit){
			warning.SetActive(true);
		}
	}

	void okFormation (GameObject go) {
		if(currentFormation+1 <= credit && TabPersosJeu[persoFormation].formation != true){
			TabPersosJeu[persoFormation].formation = true;
			TabPersosJeu[persoFormation].xp1 += TabFormations[currentFormation].nbPoints;
			TabPersosJeu[persoFormation].xp2 += TabFormations[currentFormation].nbPoints;
			TabPersosJeu[persoFormation].xp3 += TabFormations[currentFormation].nbPoints;
			TabPersosJeu[persoFormation].xp4 += TabFormations[currentFormation].nbPoints;
			TabPersosJeu[persoFormation].tempsFormation = Time.time + (float)TabFormations[currentFormation].tempsRequis;
			credit -= currentFormation+1;
			clickFormation = false;
			Time.timeScale = 1.0f;
			SelectionFormation.SetActive(false);
		}
		else{
			SelectionFormation.SetActive(false);
			clickFormation = false;
			Time.timeScale = 1.0f;
		}
		warning.SetActive(false);
		FormationMenuOpen = false;
	}
	void annulerFormation (GameObject go) {
		SelectionFormation.SetActive(false);
		clickFormation = false;
		Time.timeScale = 1.0f;
		warning.SetActive(false);
		FormationMenuOpen = false;
	}

	void Formation(){
		menu.FormationDesc.text = "Tu peux envoyer tes étudiants en formation mais attention cela coute du temps et de l'argent ! plus la formation coûte chère plus l'étudiant sera inactif sur le projet. Argent... ARGENT !!!!\nChoisissez la formation pour " + TabPersosJeu[persoFormation].name;
		SelectionFormation.SetActive(true);
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


		menu.Formation1.text = TabFormations[0].name +" \nCette formation nécessite 1 crédits et durera " + TabFormations[0].tempsRequis + " secondes pour un gain de " + TabFormations[0].nbPoints + " points";
		menu.Formation2.text = TabFormations[1].name + "\nCette formation nécessite 2 crédits et durera " + TabFormations[1].tempsRequis + " secondes pour un gain de " + TabFormations[1].nbPoints + " points";
		menu.Formation3.text = TabFormations[2].name + "\nCette formation nécessite 3 crédits et durera " + TabFormations[2].tempsRequis + " secondes pour un gain de " + TabFormations[2].nbPoints + " points";
		menu.Formation4.text = TabFormations[3].name + "\nCette formation nécessite 4 crédits et durera " + TabFormations[3].tempsRequis + " secondes pour un gain de " + TabFormations[3].nbPoints + " points";

		menu.bar1.text = TabPostes[0].name;
		menu.bar2.text = TabPostes[1].name;
		menu.bar3.text = TabPostes[2].name;
		menu.bar4.text = TabPostes[3].name;
	
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
			//FIN OU NON
			if( (Time.time - projetTime) > TabProjets[curProject].tempsRequis) End(false);
			if(Projet1Ended && Projet2Ended && Projet3Ended && Projet4Ended )
				End(true);

			//EVOLUTION AFFICHAGE CHRONO
			menu.Chrono.text =  (Time.time - projetTime).ToString("0.00") + " / "  + TabProjets[curProject].tempsRequis.ToString(); 

			//LANCEMENT D'UN TICK, UPDATE DES VARIABLES
			if (Time.time > nextActionTime ) { //Toute les 0.1 secondes 
				nextActionTime += period;
				tick ();
			}

			//SI CLICK SUR PERSO OUVETURE D'UNE INTEFACE
			if(clickPerso){
				SelectionPoste.SetActive(true);
			}
			if(clickFormation){
				Formation();
			}
			if(Input.GetKeyUp(KeyCode.Escape)){
				if(pauseIsActive){
					Time.timeScale = 1.0f;
					pauseIsActive = false;
					Pause.SetActive(false);
				}
				else{
					Time.timeScale = 0.0f;
					pauseIsActive = true;
					Pause.SetActive(true);
				}
			}
			//lancement aléatoire d'énigme INTERFACE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//sauvegarde données 
		}
	}



	void OpenEnigme(){
		if(EnigmesUsed.ToArray().GetLength(0) < TabEnigmes.GetLength(0)){
			Time.timeScale = 0.0f;
			QTemp = -1;
			do{
				QTemp = Random.Range(0,TabEnigmes.GetLength(0));
			}
			while(EnigmesUsed.Contains(QTemp));
			EnigmesUsed.Add(QTemp);
			menu.Question.text = TabEnigmes[QTemp].description;
			menu.Q1.text = TabEnigmes[QTemp].Q1;
			menu.Q2.text = TabEnigmes[QTemp].Q2;
			menu.Q3.text = TabEnigmes[QTemp].Q3;
			menu.Q4.text = TabEnigmes[QTemp].Q4;
			MenuEnigme.SetActive(true);
		}
	}

	void R1(GameObject go){
		if(TabEnigmes[QTemp].bonneReponse == 1){
			credit ++;
		}
		else{
			
		}
		MenuEnigme.SetActive(false);
		Time.timeScale = 1.0f;
	}
	void R2(GameObject go){
		if(TabEnigmes[QTemp].bonneReponse == 2){
			credit ++;
		}
		else{
			
		}
		MenuEnigme.SetActive(false);
		Time.timeScale = 1.0f;
	}
	void R3(GameObject go){
		if(TabEnigmes[QTemp].bonneReponse == 3){
			credit ++;
		}
		else{
			
		}
		MenuEnigme.SetActive(false);
		Time.timeScale = 1.0f;
	}
	void R4(GameObject go){
		if(TabEnigmes[QTemp].bonneReponse == 4){
			credit ++;
		}
		else{
			
		}
		MenuEnigme.SetActive(false);
		Time.timeScale = 1.0f;
	}

	void init(){
		TabPlaces = places.GetPlaces();
		for( int i=0; i<nbPersoSelec ; i++){
			evolution[i] = 1000;
			persoPrefab[i] = Instantiate(Resources.Load("Productions3D/Personnage/Perso/PersoPrefab")) as GameObject;
			persoPrefab[i].GetComponent<onClick>().perso = i;
			persoPrefab[i].transform.position = TabPlaces[i].transform.position;
			persoPrefab[i].transform.rotation = TabPlaces[i].transform.rotation;
			persoPrefab[i].GetComponentInChildren<ParticleSystem>().Stop();
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


	public string GetName( int i){
		return TabPersosJeu[i].name.ToString();
	}

	public bool GetFormation ( int i){
		return TabPersosJeu[i].formation;
	}



	void tick(){

		int rngEnigme;
		rngEnigme = Random.Range(0,1000);
		if(rngEnigme == 100) OpenEnigme();

		for(int i=0 ; i<nbPersoSelec ; i++){

			if(TabPersosJeu[i].tempsFormation <= Time.time) TabPersosJeu[i].formation = false;


			if(!TabPersosJeu[i].formation){  				//Si le joueur n'est pas en formation il peut généré des points
				TabPersosJeu[i].xp += Random.Range(1,10);

				if(TabPersosJeu[i].xp >= evolution[i]){
					TabPersosJeu[i].levelXp += 1;
					TabPersosJeu[i].xp = 0;
					evolution[i] += Random.Range(70,100);
					switch(TabPersosJeu[i].poste){
					case 1:
						TabPersosJeu[i].xp1 += Random.Range(5,10);
						break;
					case 2:
						TabPersosJeu[i].xp2 += Random.Range(5,10);
						break;
					case 3:
						TabPersosJeu[i].xp3 += Random.Range(5,10);
						break;
					case 4:
						TabPersosJeu[i].xp4 += Random.Range(5,10);
						break;
					}
					print(TabPersosJeu[i].name + " passe au niveau " + TabPersosJeu[i].levelXp);
					persoPrefab[i].GetComponentInChildren<ParticleSystem>().Emit(1);
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
		
		float percentTemp = xpProjetJeu1 / (float)TabProjets[curProject].xpPoste1;
		sliderPoste1.sliderValue = percentTemp;
		
		percentTemp = xpProjetJeu2 / (float)TabProjets[curProject].xpPoste2;
		sliderPoste2.sliderValue = percentTemp;
		
		percentTemp = xpProjetJeu3 / (float)TabProjets[curProject].xpPoste3;
		sliderPoste3.sliderValue = percentTemp;
		
		percentTemp = xpProjetJeu4 / (float)TabProjets[curProject].xpPoste4;
		sliderPoste4.sliderValue = percentTemp;
	}

	public void SetPosteSelected(){
		if(!clickPerso){
			PosteIsSelected = true;
			nextActionTime = Time.time;
			projetTime = nextActionTime;
		}
		clickPerso = false;
		Time.timeScale = 1.0f;
	}

	void End( bool victoire){
		//INTERFACE FIN !!!!!!!!!!!
		Time.timeScale = 0.0f;
		if(victoire){
			EndVictoire.SetActive(true);
		}
		else{
			EndDefaite.SetActive(true);
		}
	}



	public void setProjet(int i){
		curProject = i;
		ProjetIsSelected = true;
		menu.menuPauseDesc.text = TabProjets[curProject].description;
		SelectionPerso.SetActive(true);
	}



	public void setPerso(int i){
		deletedPerso = i;
		PersoIsSelected = true;
		SelectionPerso.SetActive(false);
		SelectionPoste.SetActive(true);
	}



	void OnGUI () {
		
		/*GUI.Box(new Rect(50,40,100,20), xpProjetJeu1.ToString("0") + " Poste1");
		GUI.Box(new Rect(50,60,100,20), xpProjetJeu2.ToString("0") + " Poste2");
		GUI.Box(new Rect(50,80,100,20), xpProjetJeu3.ToString("0") + " Poste3");
		GUI.Box(new Rect(50,100,100,20), xpProjetJeu4.ToString("0") + " Poste4");*/
		// Make a background box
		/*if(!ProjetIsSelected)
			GUI.Box(new Rect(5,5,Screen.width - 5,Screen.height - 5), "Choisie ton projet");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		/*if(GUI.Button(new Rect(50,40,80,20), "Level 1")) {
			Application.LoadLevel(1);
		}*/
	}


	void OnSelectionChange0 (string val)
	{
		switch (val)
		{
		case "Dévloppeur":	TabPersosJeu[0].poste = 1;	break;
		case "Logistique":	TabPersosJeu[0].poste = 2;	break;
		case "Design":	TabPersosJeu[0].poste = 3;	break;
		case "Ressources 3D":	TabPersosJeu[0].poste = 4;	break;
		}
		print (TabPersosJeu[0].poste.ToString());
	}
	void OnSelectionChange1 (string val)
	{
		switch (val)
		{
		case "Dévloppeur":	TabPersosJeu[1].poste = 1;	break;
		case "Logistique":	TabPersosJeu[1].poste = 2;	break;
		case "Design":	TabPersosJeu[1].poste = 3;	break;
		case "Ressources 3D":	TabPersosJeu[1].poste = 4;	break;
		}
	}
	void OnSelectionChange2 (string val)
	{
		switch (val)
		{
		case "Dévloppeur":	TabPersosJeu[2].poste = 1;	break;
		case "Logistique":	TabPersosJeu[2].poste = 2;	break;
		case "Design":	TabPersosJeu[2].poste = 3;	break;
		case "Ressources 3D":	TabPersosJeu[2].poste = 4;	break;
		}
	}
	void OnSelectionChange3 (string val)
	{
		switch (val)
		{
		case "Dévloppeur":	TabPersosJeu[3].poste = 1;	break;
		case "Logistique":	TabPersosJeu[3].poste = 2;	break;
		case "Design":	TabPersosJeu[3].poste = 3;	break;
		case "Ressources 3D":	TabPersosJeu[3].poste = 4;	break;
		}
	}
	void OnSelectionChange4 (string val)
	{
		switch (val)
		{
		case "Dévloppeur":	TabPersosJeu[4].poste = 1;	break;
		case "Logistique":	TabPersosJeu[4].poste = 2;	break;
		case "Design":	TabPersosJeu[4].poste = 3;	break;
		case "Ressources 3D":	TabPersosJeu[4].poste = 4;	break;
		}
	}
}
