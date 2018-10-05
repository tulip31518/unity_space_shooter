using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

	public Vector2 startWait;
	public Vector2 manaueverTime;
	public Vector2 manaueverWait;
	public Boundary boundary;
	public float dodge;
	public float smoothing;
	public float tilt;

	private float targetManauever;
	private Rigidbody rb;
	private float currentSpeed;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine (Evade());
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float newManauever = Mathf.MoveTowards (rb.position.x, targetManauever, Time.deltaTime * smoothing);
		rb.velocity = new Vector3(newManauever, 0f, currentSpeed);
		rb.position = new Vector3 
		(
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	IEnumerator Evade()
	{
		yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));

		while (true) 
		{
			targetManauever = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range(manaueverTime.x, manaueverTime.y));
			targetManauever = 0;
			yield return new WaitForSeconds (Random.Range(manaueverWait.x, manaueverWait.y));
		}
	}
}
