using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardManager : MonoBehaviour {

    /*
     * Card manager class handles controlling when cards are selected by the player and when to draw new cards. Script provides instructions and information 
     * to other classes to contribute to gameplay
     */

#region Instance Variables
    //Inspector Variables/////////////////////////////////////////////
    [Header("Card Controls")]                                       //
    [SerializeField]
    private int maxSelectedPerTrun;                                 // Maximum number of cards that can be selected in one turn
    [SerializeField]
    private bool mustApplyAllThrust;                                // Determines if the player must use all available thrust points
    [SerializeField]
    private float shieldTime;                                       // Time that a single shield will stay active for

    [Space(5)]
    [Header("Card Rates")]
    [SerializeField]
    private float attackCardRate;                                   // Rate at which the attack card is drawn
    [SerializeField]
    private float shieldCardRate;                                   // Rate at which the shield card is drawn
    [SerializeField]
    private float movementCardOneRate;                              // Rate at which the level 1 movement card is drawn
    [SerializeField]
    private float movementCardTwoRate;                              // Rate at which the level 2 movement card is drawn
    [SerializeField]
    private float movementCardThreeRate;                            // Rate at which the level 3 movement card is drawn

    [Space(5)]
    [Header("Movement Card Options")]
    [SerializeField]
    private int levelOneThrust;                                     // Amount of thrust movement card 1 applies
    [SerializeField]
    private int levelTwoThrust;                                     // Amount of thrust movement card 2 applied
    [SerializeField]
    private int levelThreeThrust;                                   // Amount of thrust movement card 3 applies
    [SerializeField]
    private int thrustMultFactor;                                   // Factor value to be multiplied by thrust player applied

    [Space(5)]
    [Header("Bullet Options")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed;

    [Space(5)]
    [Header("General Controls")]
    [SerializeField]
    private float gameActiveDuration;                               // Amount of time set by the developer for the game to be "active" for
    [SerializeField]
    private Color defaultCardColour;                                // Default colour used on selection cards
    [SerializeField]
    private Color selectedCardColour;                               // Colour used when a card is selected

    [Space(5)]
    [Header("Object References")]
    [SerializeField]
    private GameObject player;                                      // Reference to player object
    [SerializeField]
    private GameObject cardsSelectionCanvas;
    [SerializeField]
    private GameObject applyThrustCanvas;
    [SerializeField]
    private GameObject tooManyCardsWarningGUI;                      // Object to warn players they have too many cards selected
    [SerializeField]
    private GameObject confirmationButton;                          // Button to confirm selected cards
    [SerializeField]
    private GameObject[] cardVisuals;                               // Array of the visuals for cards. Max number of cards is infered from this array length
    //Inspector Variables/////////////////////////////////////////////

    //General Variables///////////////////////////////////////////////
    private float maxWeightValue;                                   // Max weighted value possible from all spawn rate values in the inspector

    private float attackCardCap;                                    // Top value representing attack card number for random selection
    private float shieldCardCap;                                    // Top value representing shield card number for random selection
    private float movementOneCap;                                   // Top value representing movement 1 card number for random selection
    private float movementTwoCap;                                   // Top value representing movement 2 card number for random selection
                                                                    // NOTE - Movement 3 is not included as code logic accounts for it as default case
    private float gameplayTimer;                                    // Time for which the game is in an "active" state

    private bool gameActive;                                        // Helps code to determine when functions should run depending on if the player is active or drawing cards

    private int[] cardsArray;                                       // Array holding cards - 0/CardUsed  1/Attack  2/Shield  3/MoveOne  4/MoveTwo  5/MoveThree
    private List<int> selectedCardsList;                            // List holding all the cards currently selected by the player
    //General Variables///////////////////////////////////////////////

    #endregion

#region Unity Default Methods

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called once at start of scene. Handles initial referencing and value setting
    void Start () {
        //Set up segments for weighted values during random number selection
        //Set up the random card draw ranges
        maxWeightValue = attackCardRate + shieldCardRate + movementCardOneRate + movementCardTwoRate + movementCardThreeRate;
        attackCardCap = attackCardRate;
        shieldCardCap = attackCardRate + shieldCardRate;
        movementOneCap = shieldCardCap + movementCardOneRate;
        movementTwoCap = movementOneCap + movementOneCap;

        //Initialize the list
        selectedCardsList = new List<int>();

        //Start the gameplay timer at full
        gameplayTimer = gameActiveDuration;

        //Initialize the cardsArray
        cardsArray = new int[cardVisuals.Length];

        //Start the game as choosing cards
        gameActive = false;
        createNewDeck();

        //Make sure the right canvas is active
        cardsSelectionCanvas.SetActive(true);
        applyThrustCanvas.SetActive(false);

        //Start all the buttons with the default colour
        foreach (GameObject card in cardVisuals)
        {
            Button b = card.GetComponent<Button>();
            ColorBlock cb = b.colors;
            cb.normalColor = defaultCardColour;
        }

        //Log any errors that have occurred
        logVariableStatus();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called every frame. Handles event checking
    void Update () {
        
        
        
        
        /*//When the game is active
		if (gameActive)
        {
            //When the timer is above 0
            if (gameplayTimer > 0)
            {
                //Decriment the timer in real time
                gameplayTimer -= Time.deltaTime;
            }
            else
            {
                //End the active turn and start cards selection
                startCardSelectionPeriod();
            }
        }*/
	}

    #endregion

#region Custom Private Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Returns if the cardsArray has any cards in it
    private bool checkForNoCards ()
    {
        //Iterate through the cardsArray
        for (int i = 0; i < cardsArray.Length; i++)
        {
            //If there is a card in the deck
            if (cardsArray[i] != 0)
            {
                //Return that the deck is not empty
                return false;
            }
        }
        //If we get to here then the deck is empty
        return true;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called by start to check status of variables in the class. Method issues warning and errors if any variable has a bad value set to it. Primarily used to make
    //sure that inspector variables have no errors set in them
    private void logVariableStatus ()
    {
        //Make sure inspector values stay within a valid range
        if (attackCardRate < 0) { attackCardRate = 0; }
        if (shieldCardRate < 0) { shieldCardRate = 0; }
        if (movementCardOneRate < 0) { movementCardOneRate = 0; }
        if (movementCardTwoRate < 0) { movementCardTwoRate = 0; }
        if (movementCardThreeRate < 0) { movementCardThreeRate = 0; }

        //Error Logging
        if (cardVisuals.Length < 1) { Debug.LogError("DEVELOPER ERROR - Bad Array - No cards have been provided to the cardVisuals array. No cards will be drawn. :: " + gameObject.name); }
        if (maxSelectedPerTrun > cardVisuals.Length) { Debug.LogError("DEVELOPER ERROR - Bad Variable - maxCards is less than the max selected per turn and will cause errors :: " + gameObject.name); }
        if (maxSelectedPerTrun < 1) { Debug.LogError("DEVELOPER ERROR - Bad Variable - maxSelectedCards has not been set to a valid number :: " + gameObject.name); }
        if (levelOneThrust <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - levelOneThrust has not been set to a valid number :: " + gameObject.name); }
        if (levelTwoThrust <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - levelTwoThrust has not been set to a valid number :: " + gameObject.name); }
        if (levelThreeThrust <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - levelThreeThrust has not been set to a valid number :: " + gameObject.name); }
        if (maxWeightValue <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - maxWeight value detected with illegal value. Current value will not create any cards for the game :: " + gameObject.name); }
        if (cardsSelectionCanvas == null) { Debug.LogError("DEVELOPER ERROR - Null Variable - Unable to find the 'Available Cards Canvas' :: " + gameObject.name); }
        if (applyThrustCanvas == null) { Debug.LogError("DEVELOPER ERROR - Null Variable - Unable to find the 'Apply Thrust Canvas' :: " + gameObject.name); }
        if (gameActiveDuration <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - gameActiveDuration has not been set to a valid number and will prevent gameplay :: " + gameObject.name); }
        if (tooManyCardsWarningGUI == null) { Debug.LogError("DEVELOPER ERROR - Null Variable - selectionWarning canvas element has not been set :: " + gameObject.name); }
        if (confirmationButton == null) { Debug.LogError("DEVELOPER ERROR - Null Variable - confirmationButton canvas element has not been set :: " + gameObject.name); }

        //Warning Logging
        if (attackCardRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for attack cards has been set to 0 and will not spawn any cards of this type!"); }
        if (shieldCardRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for shield cards has been set to 0 and will not spawn any cards of this type!"); }
        if (movementCardOneRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for movement card one has been set to 0 and will not spawn any cards of this type!"); }
        if (movementCardTwoRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for movement card two has been set to 0 and will not spawn any cards of this type!"); }
        if (movementCardThreeRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for movement card three has been set to 0 and will not spawn any cards of this type!"); }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //When called the method will overwrite the cards array with a new set of cards
    private void createNewDeck ()
    {
        for (int i = 0; i < cardsArray.Length; i++)
        {
            cardsArray[i] = drawCard();
        }
        nameCards();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Chooses a random number betweeon 0 and the maxWeightValue. Depending on the section it is in, the method will return the number of which card was drawn
    //This system is used to allow for adaptable weighted random rolling. Gives developer greater control from the inspector panel.
    private int drawCard ()
    {
        //Choose random number between 0 and maxWeightValue
        float rolled = Random.Range(0, maxWeightValue);

        //Figure out which card the random number represents
        if (rolled < attackCardCap)
        {
            return 1;
        }
        else if (rolled < shieldCardCap)
        {
            return 2;
        }
        else if (rolled < movementOneCap)
        {
            return 3;
        }
        else if (rolled < movementTwoCap)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Checks each element of the list and returns if there is a duplicate of the provided value
    private bool cardExistsInList (int entry)
    {
        //Check all the cards in the list
        foreach (int card in selectedCardsList)
        {
            //If the number already exists then we return that it is already there
            if (card == entry)
            {
                return true;
            }
        }
        //If we get to here then no duplicates were found
        return false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Changes the colour of a button based on the provided parameters
    private void changeButtonColour (Image i, Color c)
    {
        i.color = c;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Names all the cards based on the card type drawn
    private void nameCards ()
    {
        Debug.Log("Naming Card:");
        for (int i = 0; i < cardsArray.Length; i++)
        {
            string name;

            switch (cardsArray[i])
            {
                case 1:
                    {
                        name = "Attack";
                        break;
                    }
                case 2:
                    {
                        name = "Shield";
                        break;
                    }
                case 3:
                    {
                        name = "Move 1";
                        break;
                    }
                case 4:
                    {
                        name = "Move 2";
                        break;
                    }
                case 5:
                    {
                        name = "Move 3";
                        break;
                    }
                default:
                    {
                        name = "Something Broken";
                        break;
                    }
            }
            Debug.Log(name);
            cardVisuals[i].transform.GetChild(0).GetComponent<Text>().text = name;
        }
    }
    #endregion

    #region Custom Public Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called to end the active portion of the game. Pauses all events and allows players to select cards
    public void startCardSelectionPeriod()
    {
        //Pause and restart active variable
        Time.timeScale = 0f;
        gameActive = false;
        gameplayTimer = gameActiveDuration;

        player.GetComponent<playerController>().endActiveRound();

        //If we've run out of cards then create a new deck
        if (checkForNoCards())
        {
            createNewDeck();

            //Turn all the cards back on for a new deck
            foreach (GameObject card in cardVisuals)
            {
                card.SetActive(true);
                changeButtonColour(card.GetComponent<Image>(), defaultCardColour);
            }
        }

        //Set up canvas'
        applyThrustCanvas.SetActive(false);
        cardsSelectionCanvas.SetActive(true);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by buttons in the scene for when the player tries to select a card
    public void selectCard (int cardNumber)
    {
        //If the card has previously been selected
        if (cardExistsInList(cardNumber))
        {
            //Remove it from the list and change the colour back to default
            selectedCardsList.Remove(cardNumber);
            changeButtonColour(cardVisuals[cardNumber].GetComponent<Image>(), defaultCardColour);
        }
        //Otherwise this is a new selection
        else
        {
            //Add it to the list and update the button colour
            selectedCardsList.Add(cardNumber);
            changeButtonColour(cardVisuals[cardNumber].GetComponent<Image>(), selectedCardColour);
        }

        //Check to see if the player has selected too many cards
        if (selectedCardsList.Count > maxSelectedPerTrun)
        {
            //Enable the warning canvas element
            tooManyCardsWarningGUI.SetActive(true);
        }
        //Otherwise they're fine
        else
        {
            //Disable the warning canvas element
            tooManyCardsWarningGUI.SetActive(false);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Returns the object reference for the applyThrustCanvas
    public GameObject getThrustCanvas ()
    {
        return applyThrustCanvas;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    public float getActiveTimer ()
    {
        return gameActiveDuration;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by Confirm Selection button in the scene to allow the player to start applying their selected cards for the next active period
    public void confirmSelection ()
    {
        //Make sure they have less than the max number of cards selected
        if (selectedCardsList.Count <= maxSelectedPerTrun)
        {
            //Turns off all selected cards once confirmed
            foreach (int cardNum in selectedCardsList)
            {
                cardVisuals[cardNum].SetActive(false);
            }

            //Turn off the cards selection Canvas
            cardsSelectionCanvas.SetActive(false);
            applyThrustCanvas.SetActive(true);

            //Send the card info to the playerController to apply chosen cards
            player.GetComponent<playerController>().getBulletInfo(bullet, bulletSpeed);
            player.GetComponent<playerController>().applyCards
                (selectedCardsList, cardsArray, applyThrustCanvas, levelOneThrust, levelTwoThrust, levelThreeThrust, thrustMultFactor, mustApplyAllThrust, shieldTime);

            //Remove the selected cards from the array
            foreach (int card in selectedCardsList)
            {
                cardsArray[card] = 0;
            }

            //Empty the cards list
            selectedCardsList = new List<int>();
        }
    }
    #endregion
}
