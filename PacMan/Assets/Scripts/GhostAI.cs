using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour {
	public float speed = 10.0f;
	public float obstacleRange = 5.0f;

	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0, speed * Time.deltaTime);
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		
		if (Physics.SphereCast(ray, 0.75f, out hit)) {
			if(hit.distance < obstacleRange) {
				float angle = Random.Range(-90, 90);
				transform.Rotate(0, angle, 0);
			}
		}
	

		
		
	}
}
