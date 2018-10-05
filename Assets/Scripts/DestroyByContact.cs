﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;
	public int newScore;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} else {
			Debug.LogError ("Cannot find GameController Object.");
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary")
			return;
		Instantiate (explosion, transform.position, transform.rotation);
		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		gameController.AddScore (newScore);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
