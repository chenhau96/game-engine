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
	
	// Use this for initialization
	void Start () {
		// Set initial value for score and lives
		score = 0;
		lives = 3;
		
		// Set initial UI text for score and lives
		scoreText.text = "Score: " + score;
		livesText.text = "Lives: " + lives;
		
		
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
	
	public void AddScore() {
		score++;
		scoreText.text = "Score: " + score;
		Debug.Log(food.Length);
	}
	
	public void ReduceLives() {
		lives--;
		
		if (lives <= 0)
			gameLost();
		
		livesText.text = "Lives: " + lives;
	}
	
	public void gameWin() {
		winText.enabled = true;
		Time.timeScale = 0f;
	}
	
	public void gameLost() {
		gameOverText.enabled = true;
		Time.timeScale = 0f;
	}
	
	//powerUp 
	public void PowerUpCollected() {
		PowerUp();
		Invoke("Normal" , 5f);
	}
	
	//Destroy ghost onTriggerEnter
	void PowerUp(){
		PacmanController.PU=true;
	}
	
	void Normal(){
		PacmanController.PU =false;
	}
}
