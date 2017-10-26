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
	
	// Update is called once per frame
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
			for (int i = 0; i < 7; i ++)
			{
				foreach(var player in Players)
				{
					player.Play(i);
				}
			}
		}
	}
}
