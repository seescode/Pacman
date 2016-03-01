using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoWalk : MonoBehaviour {

	private const int RIGHT_ANGLE = 90;
	public Text yText;


	// This variable determinates if the player will move or not 
	private bool isWalking = false;

	CardboardHead head = null;

	//This is the variable for the player speed
	[Tooltip("With this speed the player will move.")]
	public float speed;

	[Tooltip("Activate this checkbox if the player shall move when the Cardboard trigger is pulled.")]
	public bool walkWhenTriggered;

	[Tooltip("Activate this checkbox if the player shall move when he looks below the threshold.")]
	public bool walkWhenLookDown;

	[Tooltip("This has to be an angle from 0° to 90°")]
	public double thresholdAngle;

	[Tooltip("Activate this Checkbox if you want to freeze the y-coordiante for the player. " +
			 "For example in the case of you have no collider attached to your CardboardMain-GameObject" +
			 "and you want to stay in a fixed level.")]
	public bool freezeYPosition;

	[Tooltip("This is the fixed y-coordinate.")]
	public float yOffset;

	void Start()
	{
		head = Camera.main.GetComponent<StereoController>().Head;
		yText.text = "";
	}

	void Update()
	{
		// Walk when the Cardboard Trigger is used 
		if (walkWhenTriggered && !walkWhenLookDown && !isWalking && Cardboard.SDK.Triggered)
		{
			isWalking = true;
		}
		else if (walkWhenTriggered && !walkWhenLookDown && isWalking && Cardboard.SDK.Triggered)
		{
			isWalking = false;
		}

		// Walk when player looks below the threshold angle 
		if (walkWhenLookDown && !walkWhenTriggered && !isWalking &&
			head.transform.eulerAngles.x >= thresholdAngle &&
			head.transform.eulerAngles.x <= RIGHT_ANGLE)
		{
			isWalking = true;
		}
		else if (walkWhenLookDown && !walkWhenTriggered && isWalking &&
				 (head.transform.eulerAngles.x <= thresholdAngle ||
				 head.transform.eulerAngles.x >= RIGHT_ANGLE))
		{
			isWalking = false;
		}

		// Walk when the Cardboard trigger is used and the player looks down below the threshold angle
		if (walkWhenLookDown && walkWhenTriggered && !isWalking &&
			head.transform.eulerAngles.x >= thresholdAngle &&
			Cardboard.SDK.Triggered &&
			head.transform.eulerAngles.x <= RIGHT_ANGLE)
		{
			isWalking = true;
		}
		else if (walkWhenLookDown && walkWhenTriggered && isWalking &&
				 head.transform.eulerAngles.x >= thresholdAngle &&
				 (Cardboard.SDK.Triggered ||
				 head.transform.eulerAngles.x >= RIGHT_ANGLE))
		{
			isWalking = false;
		}

		if (isWalking)
		{
			//I think the way to tell if the ball is moving in one direction is to look at positions x and z when
			//running the application.  I think maybe if you only see it move in one direction then it works.

			Vector3 direction;

			float y = head.transform.rotation.eulerAngles.y;
			yText.text = y.ToString();
			
			if (y > 45 && y <= 135)
			{
				direction = new Vector3(head.transform.forward.x, 0, 0).normalized * speed * Time.deltaTime;
			}
			else if (y > 135 && y <= 225)
			{
				//back
				direction = new Vector3(0, 0, head.transform.forward.z).normalized * speed * Time.deltaTime;
			}
			else if (y > 225 && y <= 315)
			{
				direction = new Vector3(head.transform.forward.x, 0, 0).normalized * speed * Time.deltaTime;
			}
			else
			{
				//forward
				direction = new Vector3(0, 0, head.transform.forward.z).normalized * speed * Time.deltaTime;
			}

			//Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			transform.Translate(/*rotation * */ direction);
		}

		if (freezeYPosition)
		{
			transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
		}
	}
}
