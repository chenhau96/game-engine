using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject pacman;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
		// Get the offset between the camera position and the pacman position
        offset = transform.position - pacman.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		// Update the camera position when the pacman moves around
		// based on the offset
        transform.position = pacman.transform.position + offset;
    }
}
