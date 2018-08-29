using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
	
	public GameObject enemyPrefab;
	private GameObject[] ghosts;
	
	// Three x-coordinates in the enemy base for ghost respawning
	private int[] enemyPos = new int[] {-6, 0, 6};
	
	// To keep track the position to be respawned
	private int currIndex;
	
	// Timer to respawn ghost
	private float ghostWaitRespawnTime;
	private float ghostRespawnTime;
	
	// To keep track the number of ghost in the game
	// and only a maximum of 4 ghosts will be in the game
	private int ghostCount;
	private int maxGhostCount = 4;
	
	// Use this for initialization
	void Start () {
		// Set current index as the first position of the enemyPos array
		currIndex = 0;
		
		ghostWaitRespawnTime = 0f;
		
		// Set ghost's respawn time to 3s
		ghostRespawnTime = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		// If number of ghost in the game less than maximum ghost count,
		// start ghost respawn timer
		ghosts = GameObject.FindGameObjectsWithTag("Enemy");
		if (ghosts.Length < maxGhostCount) {
			ghostWaitRespawnTime += Time.deltaTime;
		}
		
		// If timer reaches 3 seconds, reset timer and 
		// spawn an enemy in the enemy base
		if (ghostWaitRespawnTime > ghostRespawnTime) {
			ghostWaitRespawnTime = 0f;
			spawnEnemy();
		}
	}
	
	// Spawn Enemy Function
	public void spawnEnemy() {
		// Set the spawn position of ghost
		Vector3 spawnPosition = new Vector3(enemyPos[currIndex], transform.position.y,
			transform.position.z);
		
		// Spawn a ghost
		Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
		
		currIndex++;
		
		// Reset index
		if (currIndex > 2)
			currIndex = 0;
	}
}
