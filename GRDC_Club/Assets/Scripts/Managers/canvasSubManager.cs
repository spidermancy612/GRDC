using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvasSubManager : MonoBehaviour {

    /*
     * Created by Liam Baillie
     * Feb 12, 2018
     * 
     * Class handles controlling the GUI canvas elements for the player. Tracks what player is inputing data and facilitates the shipSubManager to give
     * control data to the player ship. 
     * 
     * Controlled by the cardsManager class. 
     */

    #region Instance Variable
    //Inspector Variables/////////////////////////////////////////////
    [Header("Parent Canvases")]
    [SerializeField]
    private GameObject cardDeckCanvas;
    [SerializeField]
    private GameObject applyThrustCanvas;
    [SerializeField]
    private GameObject applyWeaponsCanvas;

    [Space(5)]
    [Header("Card Selection Options")]
    [SerializeField]
    private int maxSelectedCards;
    [SerializeField]
    private Color cardDefaultColour;
    [SerializeField]
    private Color cardSelectedColour;

    [Space(5)]
    [Header("GUI Warning Objects")]
    [SerializeField]
    private GameObject tooManyCardsWarning;

    [Space(5)]
    [Header("Card Visuals")]
    [SerializeField]
    private GameObject[] cardVisualObjectsArray;
    //Inspector Variables/////////////////////////////////////////////


    //General Variables///////////////////////////////////////////////
    private int currentPlayer;

    private bool[] availableWeapons;
    private string[] currentDeck;
    private string[] selectedCards;

    private cardDrawSubManager cardDrawManager;

    private List<int> selectedCardObjects;
    //General Variables///////////////////////////////////////////////
    #endregion


    #region Unity Default Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called at start of scene. Sets up initial states.
    private void Start()
    {
        cardDeckCanvas.SetActive(false);
        applyThrustCanvas.SetActive(false);
        applyWeaponsCanvas.SetActive(false);

        cardDrawManager = GetComponent<cardDrawSubManager>();
    }
    #endregion


    #region Private Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Loads the current deck of cards to the cardDeckCanvas for the player to select which ones to play
    private void loadCardDeckCanvas ()
    {
        //Start the GUI in the right state
        cardDeckCanvas.SetActive(true);
        tooManyCardsWarning.SetActive(false);

        //Go through the entire deck and check the card names
        for (int i = 0; i < cardVisualObjectsArray.Length; i++)
        {
            //If we're on an empty card turn off the card visual
            if (currentDeck[i] == "")
            {
                cardVisualObjectsArray[i].SetActive(false);
            }
            //Otherwise make sure the visual is on and update the text
            else
            {
                cardVisualObjectsArray[i].SetActive(true);
                cardVisualObjectsArray[i].transform.GetChild(0).GetComponent<Text>().text = currentDeck[i];
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Takes an int as a paremeter and checks if the int exists already in the List of Integers. Returns a bool for found match
    private bool isInList (int cardNum)
    {
        //Check each card in the list
        foreach (int num in selectedCardObjects)
        {
            //If we have a match then return true
            if (num == cardNum)
            {
                return true;
            }
        }
        //If we couldn't find a match then return false
        return false;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Sets the colour of a provided button to the provided colour
    private void setButtonColour (Button b, Color c)
    {
        ColorBlock cb = b.colors;
        cb.normalColor = c;
    }
    #endregion


    #region Public Methods
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by the cardDeckCanvas button for the player to confirm their card selection when ready
    public void confirmCardSelection ()
    {
        //If they have too many cards selected don't let them continue
        if (selectedCardObjects.Count > maxSelectedCards)
        {
            return;
        }

        //Tally up the cards, update the player deck with the cardDrawSubManager, load up the thrust canvas                                                                 /////////////// TODO

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called from Canvas to select or deselect a button. Sets the colour of the button when pressed depending if it has been 
    //added or removed from the selection List
    public void selectCard (int cardNum)
    {
        //Check if we should be adding or removing the card
        if (isInList(cardNum))
        {
            selectedCardObjects.Remove(cardNum);
            setButtonColour(cardVisualObjectsArray[cardNum].GetComponent<Button>(), cardDefaultColour);
        }
        else
        {
            selectedCardObjects.Add(cardNum);
            setButtonColour(cardVisualObjectsArray[cardNum].GetComponent<Button>(), cardSelectedColour);
        }

        //Update if we have too many cards selected
        if (selectedCardObjects.Count > maxSelectedCards)
        {
            tooManyCardsWarning.SetActive(true);
        }
        else
        {
            tooManyCardsWarning.SetActive(false);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by the cardsManager to start a new turn when the active portion of the game ends. Takes the current player that 
    //is selecting cards and the weapons they have available via a boolean array
    public void startNewTurn (int playerNum, bool[] weaponsAvail)
    {
        //Save the passed in data
        currentPlayer = playerNum;
        availableWeapons = weaponsAvail;

        //Get the current players deck
        currentDeck = cardDrawManager.getPlayerDeck(currentPlayer);

        //If the array sizes don't match, kill the turn and go back to the main menu
        if (currentDeck.Length != cardVisualObjectsArray.Length)
        {
            Debug.LogError("CRITICAL ERROR - Array Mismatch - Array size for player deck does not match number of card visuals given! Ending current turn!");
            SceneManager.LoadScene(0);
            return;
        }

        //Spin up a new array of cards for selection
        selectedCards = new string[maxSelectedCards];

        //Display the player deck and let them pick which ones to play
        loadCardDeckCanvas();
    }
    #endregion
}
