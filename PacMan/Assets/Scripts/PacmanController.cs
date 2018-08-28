using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour {

	public float moveSpeed = 30f;
	private Animator animator = null;
	private Vector3 up = Vector3.zero,
					right = new Vector3(0,90,0),
					down = new Vector3(0,180,0),
					left = new Vector3(0,270,0),
					currentDirection = Vector3.zero;
					
	private Vector3 initialPosition = Vector3.zero;

    // Timer for respawning the pacman when it's died
    private float deadTime = 0f;
	
	private GameController gameController;
	
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
		
		Reset();
		
		gameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		var isMoving = true;
		var isDead = animator.GetBool("isDead");

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
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("isDead", true);
			gameController.ReduceLives();
        }
		
		if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
			gameController.AddScore();
        } 
			
	}
}
