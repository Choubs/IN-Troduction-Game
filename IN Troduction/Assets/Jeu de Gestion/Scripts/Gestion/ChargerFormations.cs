using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;

public class ChargerFormations : MonoBehaviour {

	public TextAsset GameAsset;
	public Formation[] TabFormations;
	public int taille = 0;

	public void Load(){
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList formationsList = xmlDoc.GetElementsByTagName("formation"); // array of the level nodes.


		foreach (XmlNode node in formationsList)
		{
			taille++;
		}
		TabFormations = new Formation[taille];
		taille = 0;
		foreach (XmlNode node in formationsList)
		{
			TabFormations[taille] = new Formation();
			TabFormations[taille].name = node.SelectSingleNode("name").InnerText;
			TabFormations[taille].tempsRequis = int.Parse(node.SelectSingleNode("tempsRequis").InnerText);
			TabFormations[taille].formationType = int.Parse(node.SelectSingleNode("type").InnerText);
			TabFormations[taille].nbPoints = int.Parse(node.SelectSingleNode("nbPoints").InnerText);
			print ("formation "+ (taille+1) + " chargé");
			taille++;
		}
	}

	public int GetNbFormation(){
		return taille;
	}

	public Formation[] GetTabFormation(){
		return TabFormations;
	}
}
