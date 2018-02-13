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

    //Inspector Variables////////////////////////////////////////////
    [Header("Player Deck Options")]
    [SerializeField]
    private bool createNewDeckOnStart;
    [SerializeField]
    private int deckSize;

    [Space(5)]
    [Header("Card Drop Rates")]
    [SerializeField]
    private int attackRate;
    [SerializeField]
    private int shieldRate;
    [SerializeField]
    private int moveOneRate;
    [SerializeField]
    private int moveTwoRate;
    [SerializeField]
    private int moveThreeRate;

    [Space(5)]
    [Header("Card Names")]
    [SerializeField]
    private string attackName;
    [SerializeField]
    private string shieldName;
    [SerializeField]
    private string moveOneName;
    [SerializeField]
    private string moveTwoName;
    [SerializeField]
    private string moveThreeName;
    //Inspector Variables////////////////////////////////////////////


    //General Variables//////////////////////////////////////////////
    private int playerCount;
    private int cardRateSum;

    private Dictionary<int, string[]> playerDictionary;

    private string[] cardTypes;

    private GameObject persistentDataObject;
    private GameObject referenceManager;

    private persistentData persistentDataManager;
    private referenceManager sceneReferenceManager;
    //General Variables//////////////////////////////////////////////

#region Unity Default Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called once at start of scene
    void Start () {
        //Get the script references for the referenceManager and the persistentData
        persistentDataManager = GameObject.Find("PERSISTENTDATA").GetComponent<persistentData>();
        sceneReferenceManager = GameObject.Find("GAMEMANAGER").GetComponent<referenceManager>();

        //Get how many players we have
        playerCount = persistentDataManager.getPlayerCount();

        //Get the sum of all card drop rates 
        cardRateSum = attackRate + shieldRate + moveOneRate + moveTwoRate + moveThreeRate;

        //Create the dictionary of players and spin up a new card deck for each
        playerDictionary = new Dictionary<int, string[]>();
        for (int i = 0; i <= playerCount; i++)
        {
            playerDictionary.Add(i, new string[deckSize]);
        }

        //Create new decks for all players if the developer has indicated to
        if (createNewDeckOnStart)
        {
            for (int i = 1; i <= playerCount; i++)
            {
                createNewDeck(i);
            }
        }
	}
    #endregion


    #region Private Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Spins up an array of strings for what the potential cards can be. Provided to other classes in order to maintain a 
    //consistent rule for card naming across the code base
    private void loadCardStringsToArray ()
    {
        cardTypes = new string[5];

        cardTypes[0] = attackName;
        cardTypes[1] = shieldName;
        cardTypes[2] = moveOneName;
        cardTypes[3] = moveTwoName;
        cardTypes[4] = moveThreeName;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Checks the deck of the specified player for any non empty strings and returns a boolean stating if the deck is empty or not
    private bool checkForEmptyDeck (int playerNum)
    {
        //Check each card in the string array for empty strings and return if there are any active cards
        foreach (string card in playerDictionary[playerNum])
        {
            if (card != "")
            {
                return false;
            }
        }
        //Otherwise no active cards were found and we return that the deck is empty
        return true;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Creates a new deck for the specified player
    private void createNewDeck (int playerNum)
    {
        //For each potential slot in the deck, pick and new card and save the name there
        for (int i = 0; i < deckSize; i++)
        {
            //Pick a random number
            int pickNum = (int)Random.Range(0, cardRateSum);

            //Determine and assign the chosen card
            if (pickNum < attackRate)
            {
                playerDictionary[playerNum][i] = attackName;
            }
            else if (pickNum < shieldRate)
            {
                playerDictionary[playerNum][i] = shieldName;
            }
            else if (pickNum < moveOneRate)
            {
                playerDictionary[playerNum][i] = moveOneName;
            }
            else if (pickNum < moveTwoRate)
            {
                playerDictionary[playerNum][i] = moveTwoName;
            }
            else
            {
                playerDictionary[playerNum][i] = moveThreeName;
            }
        }
    }
    #endregion


    #region Public Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Returns the string array holding the deck of the specified players
    public string[] getPlayerDeck (int playerNum)
    {
        //If the deck is empty then spin up a new one
        if (checkForEmptyDeck(playerNum))
        {
            createNewDeck(playerNum);
        }

        //Return the specified player's deck
        return playerDictionary[playerNum];
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Returns the array of strings for what each card is represented by in the player deck arrays
    //Allows for a uniform version of the card names across classes
    public string[] getCardTypes ()
    {
        return cardTypes;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //When another class is done using the player deck they call this class to return the new version of the deck to be stored
    //for the next time it is needed
    public void updatePlayerDeck (int playerNum, string[] newDeck)
    {
        playerDictionary[playerNum] = newDeck;
    }
    #endregion
}
