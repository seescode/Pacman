using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Text yText;

	public Text scoreDisplay;
	private int score = 0;

	private PlayerStateEnum state = PlayerStateEnum.Normal;
	

	CardboardHead head = null;

	//This is the variable for the player speed
	[Tooltip("With this speed the player will move.")]
	public float speed;

	[Tooltip("Activate this Checkbox if you want to freeze the y-coordiante for the player. " +
			 "For example in the case of you have no collider attached to your CardboardMain-GameObject" +
			 "and you want to stay in a fixed level.")]
	public bool freezeYPosition;

	[Tooltip("This is the fixed y-coordinate.")]
	public float yOffset;

	private Rigidbody rb;


	public float defaultX;
	public float defaultY;
	public float defaultZ;


	void Start()
	{
		head = Camera.main.GetComponent<StereoController>().Head;
		rb = GetComponent<Rigidbody>();
		yText.text = "";
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pellet")
		{
			Destroy(other.gameObject);
			score += 10;
			scoreDisplay.text = String.Format("Score: {0}", score);
		}
		else if (other.gameObject.tag == "Ghost")
		{
			state = PlayerStateEnum.Dead;
		}
	}

	void Update()
	{
		if (state == PlayerStateEnum.Dead)
		{
			DeathSequence();
		}
		else
		{
			Move();
		}
	}

	void DeathSequence()
	{
		Vector3 direction = new Vector3(0, 1, 0) * speed * Time.deltaTime;
		transform.Translate(direction);

		if (rb.transform.position.y >= 10)
		{
			state = PlayerStateEnum.Normal;
			rb.transform.position = new Vector3(defaultX, defaultY, defaultZ);
		}
	}

	void Move()
	{
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

		RaycastHit hit;
		Ray landingRay = new Ray(rb.transform.position, direction);

		if (Physics.Raycast(landingRay, out hit, 0.5f))
		{
			if (hit.collider.gameObject.tag != "Wall")
			{
				transform.Translate(/*rotation * */ direction);
			}
		}
		else
		{
			//Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			transform.Translate(/*rotation * */ direction);
		}

		if (freezeYPosition)
		{
			transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
		}
	}
}
