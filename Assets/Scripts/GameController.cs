using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public int hazardCount;
	public Vector3 spawnValue;
	public float spawnWait;
	public float startWait;

	public Text scoreText;
	public Text restartText;
	public Text gameoverText;

	private bool gameOver;
	private bool restart;
	private int score;

	// Use this for initialization
	void Start () 
	{		
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameoverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
	
	// Update is called once per frame
	void Update () {
		if (restart && Input.GetKeyDown(KeyCode.R)) 
		{
			SceneManager.LoadScene ("Main");
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true) 
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}

			if (gameOver) 
			{
				restartText.text = "Press 'R' for Restart.";
				restart = true;
				break;
			}
		}
	}

	public void GameOver()
	{
		gameOver = true;
	}

	public void AddScore(int scoreValue)
	{
		score += scoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
