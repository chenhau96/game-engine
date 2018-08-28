using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour {
	
	// The amplitude and the frequency of the floating
	public float amplitude = 0.5f;
	public float frequency = 1f;
	
	Vector3 posOffset;
	Vector3 tempPos;
	
	void Start() {
		// Store the starting position of the object
		posOffset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Float up/down using Mathf.Sin()
		tempPos = posOffset;
		tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
		
		transform.position = tempPos;
	}
}
