using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movePressButton : MonoBehaviour {
	[Tooltip ("Object references for buttons")]
	public GameObject[] inputField = new GameObject[6];


	public void checkValues () {
		
		for (int i = 0; i < inputField.Length; i++) {
			Debug.Log (i + ": "+ inputField[i].GetComponent<InputField>().text);
		}
	}
}
