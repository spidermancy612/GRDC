using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardChecker: MonoBehaviour {
	private int moveTotal = 0;
	private int shieldTotal = 0;
	private int attackTotal = 0;

	public GameObject[] cards;

	public void GetCards (GameObject[] cards) {
		cards = new GameObject[3];
		//get array from dylan
		if (cards.Length < 3 || cards.Length == 1)
			Debug.Log ("Bad amount of cards");
		else {
			for (int i = 0; i < cards.Length; i++) {
				switch (cards[i].cardType) {
				case 0: //attack card was drawn
					attackTotal++;
					break;

				case 1: //shield card was drawn
					shieldTotal++;
					break;

				case 2://slow move card was drawn
					moveTotal++;
					break;
				
				case 3://med move card was drawn
					moveTotal += 2;
					break;

				case 4://fast move card was drawn
					moveTotal += 3;
					break;
				} 
			}
		}
	}
}
