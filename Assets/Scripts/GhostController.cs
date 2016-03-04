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
		direction = relativePos * Time.deltaTime;

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
				direction = new Vector3(Random.value, 0, Random.value).normalized*Time.deltaTime;
			}
		}
		else
		{
			transform.Translate(direction);
		}

	}
}
