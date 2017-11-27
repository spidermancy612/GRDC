using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardChecker: MonoBehaviour {
	private int moveTotal = 0;
	private int shieldTotal = 0;
	private int attackTotal = 0;

	public int NUMBEROFCARDS = 3;

	public int moveOnePoints = 2;
	public int moveTwoPoints = 4;
	public int moveThreePoints = 6;

	public int[] cards;

	private void Start() 
	{
		cards = new int[NUMBEROFCARDS];
		for(int i = 0; i < cards.Length; i++)
			cards [i] = -1;
		
	}
		
	public void GetCards () 
	{
		if (cards.Length != NUMBEROFCARDS || cards.Length != 1)
			Debug.LogError("Developer Error - Bad public variable - number of cards variable on " + gameObject.name);
		else
		{
			for (int i = 0; i < cards.Length; i++) 
			{
				switch (cards[i])
				{
				case -1://empty card that doesn't exist
					Debug.Log ("Empty card");
					continue;

				case 0: //attack card was drawn
					attackTotal++;
					break;

				case 1: //shield card was drawn
					shieldTotal++;
					break;

				case 2://slow move card was drawn
					moveTotal+= moveOnePoints;
					break;
				
				case 3://med move card was drawn
					moveTotal += moveTwoPoints;
					break;

				case 4://fast move card was drawn
					moveTotal += moveThreePoints;
					break;

				default://not suppose to be a card
					Debug.Log ("What the fuck is this card");
					break;
				} 
			}
		}
		//apply attack values
		if (attackTotal >= 0)
		{
			
		}

		//apply shield values
		if (shieldTotal >= 0)
		{
			
		}

		//apply move values
		if (moveTotal >= 0) 
		{
			
		}

		//reset the cards to empty
		cards [0] = -1;
		cards [1] = -1;
		cards [2] = -1;

		//reset the total points for each cards
		moveTotal = 0;
		shieldTotal = 0;
		attackTotal = 0;


	}
}
