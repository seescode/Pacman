using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour
{
	public Transform pacman;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		//Make this ghost rotate to look at pacman
		Vector3 relativePos = pacman.position - transform.position;
		//transform.rotation = Quaternion.LookRotation(relativePos);

		//Move forward based on rotation
		///transform.position += transform.forward * Time.deltaTime;

		Vector3 direction = relativePos * Time.deltaTime;

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

	}
}
