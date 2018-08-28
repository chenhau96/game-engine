using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	public Text scoreText;
	public Text livesText;
	public Text winText;
	public Text gameOverText;
	
	private int score;
	private int lives;
	private bool gameOver;
	
	// Use this for initialization
	void Start () {
		score = 0;
		lives = 3;
		
		scoreText.text = "Score: " + score;
		livesText.text = "Lives: " + lives;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void AddScore() {
		score++;
		scoreText.text = "Score: " + score;
	}
	
	public void ReduceLives() {
		lives--;
		
		if (lives <= 0)
			gameLost();
		
		livesText.text = "Lives: " + lives;
	}
	
	public void gameLost() {
		gameOverText.enabled = true;
		Time.timeScale = 0f;
	}
}
