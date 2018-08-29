using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour {
	public float speed = 10.0f;
	public float chaseSpeed = 5.0f;
	public float obstacleRange = 5.0f;
	
	PacmanController pacmanControllerScript;

	// Use this for initialization
	void Start () {
		pacmanControllerScript = GameObject.FindObjectOfType<PacmanController>();
	}
	
	// Update is called once per frame
	void Update () {
		// Use to determine the speed of the AI when it is chasing pacman
        float step = chaseSpeed * Time.deltaTime;
		
		// Move the ghost with the speed defined
		transform.Translate(0f, 0f, speed * Time.deltaTime);
		
		//Determine the direction of ghost using ray 
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		
		
		if (Physics.SphereCast(ray, 0.75f, out hit)) {
			// Get the object hit by the ray
			GameObject hitObject = hit.transform.gameObject;

			if (hitObject.name == "Pacman") {
				// If the object is a pacman, move towards the pacman
				// This only happens when pacman is in front of the ghost
				transform.position = Vector3.MoveTowards(transform.position, 
					hitObject.transform.position, step);
			}
			else if(hit.distance < obstacleRange) {
				//When the ghost near a wall or other ghost object,
				//rotate the ghost in a degree between -90 (left) to 90 (right)
				float angle = Random.Range(-90, 90);
				transform.Rotate(0, angle, 0);
			}
			
		}
	}
}
