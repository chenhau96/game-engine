using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	// UI text
	public Text scoreText;
	public Text livesText;
	public Text winText;
	public Text gameOverText;
	
	// Variables to keep track the score and lives
	private int score;
	private int lives;
	
	// Array of GameObject Food to keep track the array length
	private GameObject[] food;
	
	// Array of GameObject ghosts
	private GameObject[] ghosts;
	
	PacmanController pacmanControllerScript;
	
	// Use this for initialization
	void Start () {
		// Set initial value for score and lives
		score = 0;
		lives = 3;
		
		// Set initial UI text for score and lives
		scoreText.text = "Score: " + score;
		livesText.text = "Lives: " + lives;
		
		pacmanControllerScript = GameObject.FindObjectOfType<PacmanController>();
	}
	
	// Update is called once per frame
	void Update () {
		// Get all the game objects with tag 'food'
		// and check its length, if the length reaches 0,
		// it means all food have been eaten by pacman,
		// the game win.
		food = GameObject.FindGameObjectsWithTag("Food");
		if (food.Length == 0) 
		{
			gameWin();
		}
	}
	
	// Add score function 
	public void AddScore() {
		// Each food pacman consumes will add 10 score
		score += 10;
		
		// Update the scoreText UI
		scoreText.text = "Score: " + score;
	}
	
	// Reduce pacman lives count function
	public void ReduceLives() {
		// reduce lives count by 1
		lives--;
		
		// If lives reaches 0, game lost
		if (lives <= 0)
			gameLost();
		
		// Update the livesText UI
		livesText.text = "Lives: " + lives;
	}
	
	// When the game is won, show the winText
	public void gameWin() {
		winText.enabled = true;
		Time.timeScale = 0f;
	}
	
	// When the game is lost, show the gameOverText
	public void gameLost() {
		gameOverText.enabled = true;
		Time.timeScale = 0f;
	}
	
	// PowerUp 
	public void PowerUpCollected() {
		PowerUp();
		
		// Change pacman's state back to normal after 5 seconds
		Invoke("Normal" , 5f);
	}
	
	// Gain the ability to destroy ghosts
	void PowerUp(){
		// Set hasPowerUp to true
		pacmanControllerScript.hasPowerUp = true;
		
		// Get all the ghost gameObjects 
		ghosts = GameObject.FindGameObjectsWithTag("Enemy");
		
		// Loop through all the ghost gameObjects
		foreach (GameObject ghost in ghosts){
			// Change the color of the ghost to lightblue color
			ghost.GetComponent<Renderer>().material.color = new Color(0, 198, 255);
		}
	}
	
	// Return pacman to normal state
	void Normal(){
		// Set hasPowerUp to false
		pacmanControllerScript.hasPowerUp = false;
		
		ghosts = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject ghost in ghosts){
			// Change the color of the ghost to its original red color
			ghost.GetComponent<Renderer>().material.color = Color.red;
		}
	}
	
	
}
