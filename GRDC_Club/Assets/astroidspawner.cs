using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroidspawner : MonoBehaviour {
	public Gameobject astroid;

	// Use this for initialization
	void Start () {
		Gameobject rock = Instantiate (astroid, transform.position, transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
