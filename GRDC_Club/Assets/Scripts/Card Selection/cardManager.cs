using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardManager : MonoBehaviour {

    /*
     * Card manager class handles controlling when cards are selected by the player and when to draw new cards. Script provides instructions and information 
     * to other classes to contribute to gameplay
     */

#region Instance Variables
    //Inspector Variables/////////////////////////////////////////////
    [Header("Card Controls")]                                       //
    [SerializeField]                                                //
    private int maxCards;                                           // Maximum number of cards that can be drawn in one hand
    [SerializeField]
    private int maxSelectedPerTrun;                                 // Maximum number of cards that can be selected in one turn

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
    private float levelOneThrust;                                   // Amount of thrust movement card 1 applies
    [SerializeField]
    private float levelTwoThrust;                                   // Amount of thrust movement card 2 applied
    [SerializeField]
    private float levelThreeThrust;                                 // Amount of thrust movement card 3 applies
    //Inspector Variables/////////////////////////////////////////////

    //General Variables///////////////////////////////////////////////
    private float maxWeightValue;                                   // Max weighted value possible from all spawn rate values in the inspector

    private float attackCardCap;                                    // Top value representing attack card number for random selection
    private float shieldCardCap;                                    // Top value representing shield card number for random selection
    private float movementOneCap;                                   // Top value representing movement 1 card number for random selection
    private float movementTwoCap;                                   // Top value representing movement 2 card number for random selection
                                                                    // NOTE - Movement 3 is not included as code logic accounts for it as default case

    private GameObject cardsSelectionCanvas;                        // Canvas for allowing the player to select cards to play on this turn
    private GameObject applyThrustCanvas;                           // Canvas showing options for players to apply thrust to their ship
    //General Variables///////////////////////////////////////////////

    #endregion

#region Unity Default Methods

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called once at start of scene. Handles initial referencing and value setting
    void Start () {
        //Set up segments for weighted values during random number selection
        maxWeightValue = attackCardRate + shieldCardRate + movementCardOneRate + movementCardTwoRate + movementCardThreeRate;
        attackCardCap = attackCardRate;
        shieldCardCap = attackCardRate + shieldCardRate;
        movementOneCap = shieldCardCap + movementCardOneRate;
        movementTwoCap = movementOneCap + movementOneCap;

        logVariableStatus();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #endregion

    #region Custom Private Methods
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
        if (maxCards < 1) { Debug.LogError("DEVELOPER ERROR - Bad Variable - maxCards has not been set to a valid number :: " + gameObject.name); }
        if (maxSelectedPerTrun > maxCards) { Debug.LogError("DEVELOPER ERROR - Bad Variable - maxCards is less than the max selected per turn and will cause errors :: " + gameObject.name); }
        if (maxSelectedPerTrun < 1) { Debug.LogError("DEVELOPER ERROR - Bad Variable - maxSelectedCards has not been set to a valid number :: " + gameObject.name); }
        if (levelOneThrust <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - levelOneThrust has not been set to a valid number :: " + gameObject.name); }
        if (levelTwoThrust <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - levelTwoThrust has not been set to a valid number :: " + gameObject.name); }
        if (levelThreeThrust <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - levelThreeThrust has not been set to a valid number :: " + gameObject.name); }
        if (maxWeightValue <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - maxWeight value detected with illegal value. Current value will not create any cards for the game :: " + gameObject.name); }

        //Warning Logging
        if (attackCardRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for attack cards has been set to 0 and will not spawn any cards of this type!"); }
        if (shieldCardRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for shield cards has been set to 0 and will not spawn any cards of this type!"); }
        if (movementCardOneRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for movement card one has been set to 0 and will not spawn any cards of this type!"); }
        if (movementCardTwoRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for movement card two has been set to 0 and will not spawn any cards of this type!"); }
        if (movementCardThreeRate == 0) { Debug.LogWarning("NOTICE - Spawn rate for movement card three has been set to 0 and will not spawn any cards of this type!"); }
    }
    #endregion

    #region Custom Public Methods

#endregion
}
