using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour {
	
	public float moveSpeed = 15f;
	public AudioClip chomp1;
	public AudioClip chomp2;
	public AudioClip Death;
	private AudioSource audio;
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
	
    // Timer for respawning the pacman when it's died
    private float deadTime = 0f;
	
	// Reference of GameController script
	private GameController gameController;
	
	//to check if Pacman has powerUp
	public static bool PU = false;
	
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
		QualitySettings.vSyncCount = 0;
		initialPosition = transform.position;
		animator = GetComponent<Animator>();
		audio = transform.GetComponent<AudioSource>();
		
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

            // Respawn the pacman in initial position after 3 seconds
            // and reset the timer
            if (deadTime > 3)
            {
                Reset();
                deadTime = 0f;
            }
        }

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
		
		transform.localEulerAngles = currentDirection;
		animator.SetBool ("isMoving",isMoving);
		if (isMoving)	
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

	}
	
	void PlayChompSound1(){
		// play audio chomp1
		audio.PlayOneShot(chomp1);
	}
	
	void PlayChompSound2(){
		// play audio chomp2
		audio.PlayOneShot(chomp2);
	}
	
	void PlayDeathSound(){
		// play audio Death
		audio.PlayOneShot(Death);
	}
	
	void OnTriggerEnter(Collider other){
		/*
		// If pacman collides with enenmy
		if (other.gameObject.CompareTag("Enemy"))
        {
			// with powerup, enemy will die
			if (PU) {
				Destroy(other.gameObject);
			}
			// without powerup, destroy enemy object and
			// set isDead to true and reduce pacman lives
			else {
				Destroy(other.gameObject);
				animator.SetBool("isDead", true);
				gameController.ReduceLives();
			}
			PlayDeathSound();
        }
		*/
		// If pacman collides food, destroy the food object
		// and add score.
		if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
			gameController.AddScore();
			PlayChompSound1();
        }
		
		// If pacman collides powerup, destroy the powerup and
		// it will gain the ability to destroy enemy
		if (other.gameObject.CompareTag("PowerUp"))
		{
			Destroy(other.gameObject);
			gameController.PowerUpCollected();
			PlayChompSound2();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		// If pacman collides with enenmy
		if (collision.gameObject.CompareTag("Enemy")) {
			// with powerup, enemy will die
			if (PU) {
				Destroy(collision.gameObject);
			}
			// without powerup, destroy enemy object and
			// set isDead to true and reduce pacman lives
			else {
				Destroy(collision.gameObject);
				animator.SetBool("isDead", true);
				gameController.ReduceLives();
			}
			PlayDeathSound();
		}
	}
}
