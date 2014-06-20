using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;

public class ChargerPersos : MonoBehaviour {

	public TextAsset GameAsset;
	public Perso[] TabPersos;
	public int taille = 0;

	public void Load(){
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList persosList = xmlDoc.GetElementsByTagName("perso"); // array of the level nodes.


		foreach (XmlNode node in persosList)
		{
			taille++;
		}
		TabPersos = new Perso[taille];
		taille = 0;
		foreach (XmlNode node in persosList)
		{
			TabPersos[taille] = new Perso();
			TabPersos[taille].name = node.SelectSingleNode("name").InnerText;
			TabPersos[taille].imagePath = node.SelectSingleNode("imagePath").InnerText;
			TabPersos[taille].cv = node.SelectSingleNode("cv").InnerText;
			TabPersos[taille].xp = int.Parse(node.SelectSingleNode("xp").InnerText);
			TabPersos[taille].levelXp = int.Parse(node.SelectSingleNode("levelXp").InnerText);
			TabPersos[taille].xp1 = int.Parse(node.SelectSingleNode("xp1").InnerText);
			TabPersos[taille].xp2 = int.Parse(node.SelectSingleNode("xp2").InnerText);
			TabPersos[taille].xp3 = int.Parse(node.SelectSingleNode("xp3").InnerText);
			TabPersos[taille].xp4 = int.Parse(node.SelectSingleNode("xp4").InnerText);
			TabPersos[taille].poste = 0;
			TabPersos[taille].formation = false;
			print ("perso "+ (taille+1) + " chargé");
			taille++;
		}
	}

	public int GetNbPerso(){
		return taille;
	}

	public Perso[] GetTabPerso(){
		return TabPersos;
	}
}
