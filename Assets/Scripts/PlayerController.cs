using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public Boundary boundary;
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().position += movement * speed;

		float boundX = Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax);
		float boundZ = Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax);
		GetComponent<Rigidbody>().position = new Vector3 (boundX, 0.0f, boundZ);
	}
}
