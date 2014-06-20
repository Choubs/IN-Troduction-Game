#pragma strict

var cameraPlayer : GameObject;

function Start () {
	
	if ( !networkView.isMine) //si ce perso ne m'appartient pas
	{
		 Destroy(cameraPlayer);
		 this.enabled = false;
	}

}

function Update () {

}