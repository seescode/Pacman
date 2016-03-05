﻿using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour
{
	public Transform pacman;

	private Rigidbody rb;

	private Vector3 direction;

	private string way;
	

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		Vector3 relativePos = pacman.position - transform.position;

		way = "south";
		direction = new Vector3(0, 0, -1).normalized * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () {


		RaycastHit hit;
		Ray landingRay = new Ray(rb.transform.position, direction);

		if (Physics.Raycast(landingRay, out hit, 0.5f))
		{
			if (hit.collider.gameObject.tag != "Wall")
			{
				transform.Translate( /*rotation * */ direction);
			}
			else
			{
				UpdateWay();

				if (way == "north")
				{
					direction = new Vector3(0, 0, 1).normalized * Time.deltaTime;
				}
				else if (way == "east")
				{
					direction = new Vector3(1, 0, 0).normalized*Time.deltaTime;
				}
				else if (way == "west")
				{
					direction = new Vector3(-1, 0, 0).normalized * Time.deltaTime;
				}
				else 
				{
					direction = new Vector3(0, 0, -1).normalized * Time.deltaTime;
				}

				transform.Translate( /*rotation * */ direction);
			}
		}
		else
		{
			transform.Translate(direction);
		}
	}


	private void UpdateWay()
	{
		var random = Random.value;

		if (way == "north")
		{
			if (random <= .5)
			{
				way = "west";
			}
			else
			{
				way = "east";
			}
		}
		else if (way == "east")
		{
			if (random <= .5)
			{
				way = "north";
			}
			else
			{
				way = "south";
			}
		}
		else if (way == "west")
		{
			if (random <= .5)
			{
				way = "north";
			}
			else
			{
				way = "south";
			}
		}
		else
		{
			if (random <= .5)
			{
				way = "west";
			}
			else
			{
				way = "east";
			}
		}
	}
}
