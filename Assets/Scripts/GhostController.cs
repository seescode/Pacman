using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour
{
	public Transform pacman;

	private Rigidbody rb;

	private Vector3 direction;

	

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		Vector3 relativePos = pacman.position - transform.position;
		direction = relativePos.normalized * Time.deltaTime;

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
				transform.Translate( /*rotation * */ direction);

				var way = Random.value;

				if (way <= .25)
				{
					direction = new Vector3(1, 0, 0).normalized * Time.deltaTime;
				}
				else if (way <= .50)
				{
					direction = new Vector3(-1, 0, 0).normalized*Time.deltaTime;

				}
				else if (way <= .75)
				{
					direction = new Vector3(0, 0, 1).normalized * Time.deltaTime;
				}
				else 
				{
					direction = new Vector3(0, 0, -1).normalized * Time.deltaTime;
				}


			}
		}
		else
		{
			transform.Translate(direction);
		}

	}
}
