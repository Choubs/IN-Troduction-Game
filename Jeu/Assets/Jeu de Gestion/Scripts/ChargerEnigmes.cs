using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;

public class ChargerEnigmes : MonoBehaviour {

	public TextAsset GameAsset;
	public Enigme[] TabEnigmes;
	public int taille = 0;

	void Start () {
		
	}

	void Update () {
	
	}

	public void Load(){
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList enigmesList = xmlDoc.GetElementsByTagName("enigme"); // array of the level nodes.


		foreach (XmlNode node in enigmesList)
		{
			taille++;
		}
		TabEnigmes = new Enigme[taille];
		taille = 0;
		foreach (XmlNode node in enigmesList)
		{
			TabEnigmes[taille] = new Enigme();
			TabEnigmes[taille].name = node.SelectSingleNode("name").InnerText;
			TabEnigmes[taille].description = node.SelectSingleNode("description").InnerText;
			TabEnigmes[taille].Q1 = node.SelectSingleNode("Q1").InnerText;
			TabEnigmes[taille].Q2 = node.SelectSingleNode("Q2").InnerText;
			TabEnigmes[taille].Q3 = node.SelectSingleNode("Q3").InnerText;
			TabEnigmes[taille].Q4 = node.SelectSingleNode("Q4").InnerText;
			TabEnigmes[taille].bonneReponse = int.Parse(node.SelectSingleNode("bonneReponse").InnerText);
			print ("enigme "+ (taille+1) + " chargé");
			taille++;
		}
	}

	public int GetNbEnigme(){
		return taille;
	}

	public Enigme[] GetTabEnigme(){
		return TabEnigmes;
	}
}
