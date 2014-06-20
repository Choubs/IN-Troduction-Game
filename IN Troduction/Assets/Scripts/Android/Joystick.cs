using UnityEngine;
using System.Collections;

public class Joystick : TouchLogic 
{
	public enum JoystickType {Movement, LookRotation, SkyColor};
	public JoystickType joystickType;
	public Transform player = null;
	public float playerSpeed = 2f, maxJoyDelta = 0.05f, rotateSpeed = 100.0f;
	private Vector3 oJoyPos, joyDelta;
	private Transform joyTrans = null;
	public CharacterController troller;
	private float pitch = 0.0f,
	yaw = 0.0f;

	private Vector3 oRotation;
	void Start () 
	{
		joyTrans = this.transform;
		oJoyPos = joyTrans.position;

		oRotation = player.eulerAngles;
		pitch = oRotation.x;
		yaw = oRotation.y;

	}
	
	void OnTouchBegan()
	{
		
		touch2Watch = TouchLogic.currTouch;
	}
	
	void OnTouchMovedAnywhere()
	{
		if(TouchLogic.currTouch == touch2Watch)
		{

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
		
		if(TouchLogic.currTouch == touch2Watch || Input.touches.Length <= 0)
		{
			
			joyTrans.position = oJoyPos;
			touch2Watch = 64;
		}
	}
	
	void ApplyDeltaJoy()
	{
		switch(joystickType)
		{ 
		case JoystickType.Movement:
			troller.Move ((player.forward * joyDelta.z + player.right * joyDelta.x) * playerSpeed * Time.deltaTime);
			//player.Translate = (player.forward * joyDelta.z + player.right * joyDelta.x) * playerSpeed * Time.deltaTime;
			break;
		case JoystickType.LookRotation:
			pitch -= Input.GetTouch(touch2Watch).deltaPosition.y * rotateSpeed * Time.deltaTime;
			yaw += Input.GetTouch(touch2Watch).deltaPosition.x * rotateSpeed * Time.deltaTime;

			//Limite pour ne pas faire de demi tour
			pitch = Mathf.Clamp(pitch, -30, 30);
			
			//rotation de la camera
			player.eulerAngles = new Vector3 ( pitch, yaw, 90.0f);
			break;
		case JoystickType.SkyColor:
			Camera.main.backgroundColor = new Color(joyDelta.x, joyDelta.z, joyDelta.x*joyDelta.z);
			break;
			
		}
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
	
	void LateUpdate()
	{
		if(!troller.isGrounded)
			troller.Move(Vector3.down * 2);

	}
}