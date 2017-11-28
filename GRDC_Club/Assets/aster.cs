using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]


public class aster : MonoBehaviour {
	public GameObject asteroid;
	private Rigidbody rb; 
	int randLeft;
	int randUp;
	int randForward;
	int randForceX;
	int randForceZ;
	double startMag;

	// Use this for initialization
	void Start () {
		randLeft = Random.Range(-100,100);
		randUp = Random.Range(-100,100);
		randForward = Random.Range(-100,100);
		randForceX = Random.Range(-200,200);
		randForceZ = Random.Range(-200,200); 

		rb = GetComponent <Rigidbody> ();
		rb.useGravity = false;
		rb.AddForce (new Vector3(randForceX, 0, randForceZ));
		rb.AddTorque (Vector3.left * randLeft);
		rb.AddTorque (Vector3.up * randUp);
		rb.AddTorque (Vector3.forward * randForward);
		startMag = Vector3.Magnitude;
	}

	// Update is called once per frame
	void Update () {
		// expand on this more (we are trying to set velocity in a reflected manner)
		if (Vector3.Magnitude < startMag) {
			rb.AddForce (rb.velocity.Normalize() * Vector3.Magnitude / (Vector3.Magnitude / startMag));
		}
	}
			/*
	private void OnCollisionEnter(Collision collision){
		rb.AddTorque (Vector3.left * -randLeft);
		rb.AddTorque (Vector3.up * -randUp);
		rb.AddTorque (Vector3.forward * -randForward);
	}*/
};
