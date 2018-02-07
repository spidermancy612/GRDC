using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardDrawSubManager : MonoBehaviour {

    /*
     * Script acts as a SubManager for the CardsManager.
     * Handles storing and drawing cards for all players.
     * 
     * Deals with when all cards are used and draws decks when needed
     */

    private int playerCount;

    private Dictionary<int, string[]> playerDictionary;


#region Unity Default Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called once at start of scene
    void Start () {
		
	}

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called every frame
    void Update () {
		
	}
    #endregion


    #region Private Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    private bool checkForEmptyDeck (int playerIndex)
    {
        return true;
    }
    #endregion


    #region Public Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    public void runInitialLoad (int pCount)
    {

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    public int[] getPlayerDeck (int playerNum)
    {
        return null;
    }
    #endregion
}
