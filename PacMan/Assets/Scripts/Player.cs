using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int playerSpeed;

	// Use this for initialization
	void Start () {
        playerSpeed = 20;
    }
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
		
		//transform.Translate(moveHorizontal, 0f, moveVertical);
		
		
		 Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		 transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement),0.15f);
		  
		  
		 transform.Translate (movement,Space.World);
		

		
	}
	



}
