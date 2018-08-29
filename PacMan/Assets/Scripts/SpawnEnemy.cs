using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
	
	public GameObject enemyPrefab;
	private GameObject[] ghosts;
	
	private int[] enemyPos = new int[] {-6, 0, 6};
	private int currIndex;
	
	private float ghostWaitRespawnTime;
	private float ghostRespawnTime;
	private int ghostCount;
	
	// Use this for initialization
	void Start () {
		currIndex = 0;
		
		ghostWaitRespawnTime = 0f;
		ghostRespawnTime = 3f;
	}
	
	// Update is called once per frame
	void Update () {	
		ghosts = GameObject.FindGameObjectsWithTag("Enemy");
		if (ghosts.Length < 4) {
			ghostWaitRespawnTime += Time.deltaTime;
		}
		
		if (ghostWaitRespawnTime > ghostRespawnTime) {
			ghostWaitRespawnTime = 0f;
			spawnEnemy();
		}
	}
	
	public void spawnEnemy() {
		Vector3 spawnPosition = new Vector3(enemyPos[currIndex], transform.position.y,
			transform.position.z);
			
		Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
		
		currIndex++;
		
		if (currIndex > 2)
			currIndex = 0;
	}
}
