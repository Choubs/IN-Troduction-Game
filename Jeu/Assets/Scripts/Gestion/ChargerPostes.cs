using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;

public class ChargerPostes : MonoBehaviour {

	public TextAsset GameAsset;
	public Poste[] TabPostes;
	public int taille = 0;

	void Start () {
		
	}

	void Update () {
	
	}

	public void Load(){
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList postesList = xmlDoc.GetElementsByTagName("poste"); // array of the level nodes.


		foreach (XmlNode node in postesList)
		{
			taille++;
		}
		TabPostes = new Poste[taille];
		taille = 0;
		foreach (XmlNode node in postesList)
		{
			TabPostes[taille] = new Poste();
			TabPostes[taille].name = node.SelectSingleNode("name").InnerText;
			TabPostes[taille].imagePath = node.SelectSingleNode("imagePath").InnerText;
			TabPostes[taille].desc = node.SelectSingleNode("description").InnerText;
			TabPostes[taille].numero = int.Parse(node.SelectSingleNode("temps").InnerText);
			print ("projet "+ (taille+1) + " chargé");
			taille++;
		}
	}

	public int GetNbPoste(){
		return taille;
	}

	public Poste[] GetTabPoste(){
		return TabPostes;
	}
}
