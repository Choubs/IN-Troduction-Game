using UnityEngine;
using System.Collections;

public class CamControl : TouchLogic 
{
	public Transform player = null;
	public float maxJoyDelta = 0.05f, rotateSpeed = 100.0f;
	public int invertPitch = 1;
	private Vector3 oJoyPos, joyDelta;
	private Transform joyTrans = null;
	private float pitch = 0.0f,
	yaw = 0.0f;
	//[NEW]//cache initial rotation of player so pitch and yaw don't reset to 0 before rotating
	private Vector3 oRotation;
	void Start () 
	{
		joyTrans = this.transform;
		oJoyPos = joyTrans.position;
		//NEW//cache original rotation of player so pitch and yaw don't reset to 0 before rotating
		oRotation = player.eulerAngles;
		pitch = oRotation.x;
		yaw = oRotation.y;
		
	}
	
	void OnTouchBegan()
	{
		//Used so the joystick only pays attention to the touch that began on the joystick
		touch2Watch = TouchLogic.currTouch;
	}
	
	void OnTouchMovedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{
			//move the joystick
			joyTrans.position = MoveJoyStick();
			ApplyDeltaJoy();
		}
	}
	
	void OnTouchStayedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{
			ApplyDeltaJoy();
		}
	}
	
	void OnTouchEndedAnywhere()
	{
		//the || condition is a failsafe so joystick never gets stuck with no fingers on screen
		if(TouchLogic.currTouch == touch2Watch || Input.touches.Length <= 0)
		{
			//move the joystick back to its orig position
			joyTrans.position = oJoyPos;
			touch2Watch = 64;
		}
	}
	
	void ApplyDeltaJoy()
	{
			pitch -= Input.GetTouch(touch2Watch).deltaPosition.y * rotateSpeed * invertPitch * Time.deltaTime;
			yaw += Input.GetTouch(touch2Watch).deltaPosition.x * rotateSpeed * invertPitch * Time.deltaTime;
			
			//limit so we dont do backflips
			pitch = Mathf.Clamp(pitch, -80, 80);
			
			//do the rotations of our camera 
			player.eulerAngles = new Vector3 ( pitch, yaw, 0.0f);
	}
	
	Vector3 MoveJoyStick()
	{
		//convert the touch position to a % of the screen to move our joystick
		float x = Input.GetTouch (touch2Watch).position.x / Screen.width,
		y = Input.GetTouch (touch2Watch).position.y / Screen.height;
		//combine the floats into a single Vector3 and limit the delta distance
		
		//If you want a rectangularly limited joystick (used in video), use this
		//Vector3 position = new Vector3 (Mathf.Clamp(x, oJoyPos.x - maxJoyDelta, oJoyPos.x + maxJoyDelta),
		//Mathf.Clamp(y, oJoyPos.y - maxJoyDelta, oJoyPos.y + maxJoyDelta), 0);//use Vector3.ClampMagnitude instead if you want a circular clamp instead of a square
		
		//If you want a circularly limited joystick, use this (uncomment it)
		Vector3 position = Vector3.ClampMagnitude(new Vector3 (x-oJoyPos.x, y-oJoyPos.y, 0), maxJoyDelta) + oJoyPos;
		
		
		//joyDelta used for moving the player
		joyDelta = new Vector3(position.x - oJoyPos.x, 0, position.y - oJoyPos.y).normalized;
		//position used for moving the joystick
		return position;
	}
}