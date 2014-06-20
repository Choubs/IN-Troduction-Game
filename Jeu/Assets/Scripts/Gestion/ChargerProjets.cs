using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;

public class ChargerProjets : MonoBehaviour {

	public TextAsset GameAsset;
	public Projet[] TabProjets;
	public int taille = 0;

	void Start () {
		
	}

	void Update () {
	
	}

	public void Load(){
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList projetsList = xmlDoc.GetElementsByTagName("projet"); // array of the level nodes.


		foreach (XmlNode node in projetsList)
		{
			taille++;
		}
		TabProjets = new Projet[taille];
		taille = 0;
		foreach (XmlNode node in projetsList)
		{
			TabProjets[taille] = new Projet();
			TabProjets[taille].typeProjet = int.Parse(node.SelectSingleNode("typeProjet").InnerText);
			TabProjets[taille].name = node.SelectSingleNode("name").InnerText;
			TabProjets[taille].imagePath = node.SelectSingleNode("imagePath").InnerText;
			TabProjets[taille].description = node.SelectSingleNode("description").InnerText;
			TabProjets[taille].tempsRequis = int.Parse(node.SelectSingleNode("temps").InnerText);
			TabProjets[taille].xpPoste1 = int.Parse(node.SelectSingleNode("xpPoste1").InnerText);
			TabProjets[taille].xpPoste2 = int.Parse(node.SelectSingleNode("xpPoste2").InnerText);
			TabProjets[taille].xpPoste3 = int.Parse(node.SelectSingleNode("xpPoste3").InnerText);
			TabProjets[taille].xpPoste4 = int.Parse(node.SelectSingleNode("xpPoste4").InnerText);
			print ("projet "+ (taille+1) + " chargé");
			taille++;
		}
	}

	public int GetNbProjet(){
		return taille;
	}

	public Projet[] GetTabProjet(){
		return TabProjets;
	}
}
