﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PacmanController : MonoBehaviour {
	
	// Audio clip for pacman action
	public AudioClip chompFoodSound;
	public AudioClip chompPowerupSound;
	public AudioClip deathSound;
	private AudioSource audio;
	
	public float moveSpeed = 15f;
	
	private Animator animator = null;
	private Vector3 up = Vector3.zero,
					right = new Vector3(0,90,0),
					down = new Vector3(0,180,0),
					left = new Vector3(0,270,0),
					currentDirection = Vector3.zero;
					
	private Vector3 initialPosition = Vector3.zero;
	
	private bool isMoving;
	private bool isDead;
	
	private GameObject[] ghost;
	
    // To keep track the duration when pacman dies
    private float deadTime = 0f;
	
	// Time required to respawn pacman (3s)
	private float respawnTime = 3f;
	
	// Reference of GameController script
	private GameController gameController;
	
	// to check if Pacman has powerUp
	public bool hasPowerUp = false;

	// Reset pacman initial state
	public void Reset(){
		// Set pacman position to starting position;
		transform.position = initialPosition;
		animator.SetBool("isDead",false);
		animator.SetBool("isMoving",false);
		currentDirection = down;
	}

	// Use this for initialization
	void Start () {
		// Set initial position of pacman using its current position
		// when the game starts, the initial position will later used
		// to respawn the pacman in this position
		initialPosition = transform.position;
		
		// Get the animation and audio components attached on pacman
		animator = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		
		// Start pacman at initial state
		Reset();
		
		// Get the reference of the GameController script
		gameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		isMoving = true;
		isDead = animator.GetBool("isDead");

        if (isDead)
        {
            // Start timer when it dies
            deadTime += Time.deltaTime;
            isMoving = false;

            // Respawn the pacman in initial position after 3 seconds,
            // reset the timer and resume the ghosts' movement
            if (deadTime > respawnTime)
            {
                Reset();
				gameController.ResumeGhostMovement();
                deadTime = 0f;
            }
        }
		
		// Arrow keys to control the pacman direction
		if (Input.GetKey(KeyCode.UpArrow)) 
			currentDirection = up;
		else if (Input.GetKey(KeyCode.RightArrow)) 
			currentDirection = right;
		else if (Input.GetKey(KeyCode.DownArrow)) 
			currentDirection = down;
		else if (Input.GetKey(KeyCode.LeftArrow)) 
			currentDirection = left;
		else 
			isMoving = false;
		
		// To determine the pacman's facing direction
		transform.localEulerAngles = currentDirection;
		
		animator.SetBool ("isMoving",isMoving);
		if (isMoving) {
			// Move pacman forward according to the direction pacman is facing
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
	}
	
	// Play audio when pacman eats a food
	void PlayChompFoodSound(){
		audio.PlayOneShot(chompFoodSound);
	}
	
	// Play audio when pacman eats a powerup
	void PlayChompPowerupSound(){
		audio.PlayOneShot(chompPowerupSound);
	}
	
	// Play audio when pacman dies
	void PlayDeathSound(){
		audio.PlayOneShot(deathSound);
	}
	
	void OnTriggerEnter(Collider other){
		// If pacman collides food, destroy the food object
		// and add score.
		if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
			gameController.AddScore();
			PlayChompFoodSound();
        }
		
		// If pacman collides powerup, destroy the powerup and
		// call PowerUpCollected() in GameController in order to have
		// the ability to destroy the enemy
		if (other.gameObject.CompareTag("PowerUp"))
		{
			Destroy(other.gameObject);
			gameController.PowerUpCollected();
			PlayChompPowerupSound();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		// If pacman collides with ghost
		if (collision.gameObject.CompareTag("Enemy")) {
			// with powerup, enemy will die
			if (hasPowerUp) {
				Destroy(collision.gameObject);
			}
			// without powerup, destroy enemy object and
			// set isDead animation to true and reduce pacman lives
			// When pacman dies, temporary stop all the ghosts' movement
			else {
				Destroy(collision.gameObject);
				animator.SetBool("isDead", true);
				gameController.ReduceLives();
				gameController.StopGhostMovement();
			}
			
			// Play death sound whenever pacman collides with ghost
			PlayDeathSound();
		}
	}

}
