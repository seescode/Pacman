using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour
{
	public Transform pacman;
	
	// Update is called once per frame
	void Update () {
		//Make this ghost rotate to look at pacman
		Vector3 relativePos = pacman.position - transform.position;
		transform.rotation = Quaternion.LookRotation(relativePos);

		//Move forward based on rotation
		transform.position += transform.forward * Time.deltaTime;
	}
}
