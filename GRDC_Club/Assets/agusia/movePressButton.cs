using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movePressButton : MonoBehaviour {

	//stores all the movement input fields
	[Tooltip ("Object references for input fields")]
	public GameObject[] inputField = new GameObject[6];

	[Tooltip ("Input field for total movements")]
	public GameObject totalMovesField;

	[Tooltip ("Total moves for the players turn")]
	public int totalMoves = 7;
	void Start() {
		//for loop that checks each value and adds it if it is a move card with the right about of points

		totalMovesField.GetComponent<InputField>().text = totalMoves.ToString();
	}

	public void checkValues () {
		int ctr = 0;
		for (int i = 0; i < inputField.Length; i++) {
			ctr += int.Parse(inputField[i].GetComponent<InputField>().text);
		}

		if (totalMoves == ctr) {
			//goto the movement
			Debug.Log("Right value for moves inputted");
		} else {
			//incorrect value do nothing
			Debug.Log("Wrong amount of points, idiot!");
		}
	}
}
