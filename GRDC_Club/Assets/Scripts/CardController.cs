using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {

	// Use this for initialization

	Vector3 loc;
	Quaternion rot;

	void Start () {
		loc = this.transform.position;
		rot = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DrawCard(){
		Animator card = GetComponent<Animator>();
		card.Play("Take001", 0);
	}

	public void Reset(){
		this.transform.position = loc;
		this.transform.rotation = rot;
	}
}
