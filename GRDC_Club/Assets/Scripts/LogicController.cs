using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicController : MonoBehaviour {

	// Use this for initialization
	public List<Player> Players;

	public CardHolderLogic cardHolder;

	private bool playTurn;

	void Start () {
		playTurn = false;
	}
	
	/// <summary>
    /// Chhecks of all players are ready
    /// if so it plays out the turn
    /// </summary>
	void Update () {
		playTurn = true;
		foreach (var player in Players)
		{
			if (!player.Ready)
			{
				playTurn = false;
			}
		}

		if(playTurn)
		{
			for (int i = 0; i < 3; i ++)
			{
				foreach(var player in Players)
				{
                    //tell player to act out turn
				}
                //delay 5 secs
			}
		}
	}
}
