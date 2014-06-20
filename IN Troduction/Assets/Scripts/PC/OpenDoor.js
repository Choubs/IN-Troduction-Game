#pragma strict
var animator : Animator;
//var bruit : AudioClip;

function Start () {
	//audio.loop = false;
	animator = GetComponentInChildren(Animator);
}

function OnTriggerEnter(){
	//audio.clip = bruit;
	//audio.Play();
	animator.SetBool("Open",true);
}
function OnTriggerExit(){
	animator.SetBool("Open",false);
}