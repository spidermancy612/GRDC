using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class playerController : MonoBehaviour {

    /*
     * TODO
     * - Public methods for updating thrust to apply
     * - Canvas for the thrust to be applied
     *      Thinking of having each button press update thrust vars and then a quick check when the press confirm
     *      Don't forget to check the allThrustNeeded boolean
     * - Code to run when the confirm button is pressed
     * - Turn the shield on for the right amount of time
     * - Shoot method needs code to spawn bullet
     * - Make sure to plug in health code
     * - I was thinking of raycasting to show the player the ship direction
     */

    #region Instance Variables

    //General Variables///////////////////////////////////////
    private int[] cardsArray;

    private GameObject shootPoint;                          // Location to spawn player projectile when firing
    private GameObject shield;                              // Player shield object
    private GameObject applyForceCanvas;                    // Canvas used to allow the player to apply force to their ship this turn

    private Rigidbody playerRigidBody;                      // Rigidbody component on the player

    private int totalThrustPoints;                          // Sum of all thrust available
    private int thrustFactor;                               // Factor to multiply thrust by
    private int timesToShoot;                               // How many times the player will shoot

    private float shieldTime;                               // How long the shield will be active for
    private float shootTimer;

    private float leftThrust;
    private float rightThrust;
    private float forwardThrust;
    private float backwardThrust;
    private float leftRotation;
    private float rightRotation;

    private bool allThrustNeeded;                           // Determines if all thrust must be applied by the player to start the active turn
    private bool roundActive;                               // Determines if the round is in an active state
    //General Variables///////////////////////////////////////
    #endregion

    #region Default Unity Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called once at start of scene. Responsible for setting up needed references
    private void Start()
    {
        //Get the shield and shoot point on this player
        assignPlayerVariables();

        //Get the rigidBody reference
        playerRigidBody = GetComponent<Rigidbody>();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    private void Update()
    {
        //Check if timer is active
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        //Otherwise we can shoot
        else
        {
            //Check if we have ammo left and the round is active
            if (timesToShoot > 0 && roundActive)
            {
                //Fire the gun and reset for next shot
                shoot();
                timesToShoot--;
                shootTimer = 1;
            }
        }
    }
    #endregion

    #region Private Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    private void assignPlayerVariables ()
    {
        //Find the shoot point from the children
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Shoot"))
            {
                shootPoint = transform.GetChild(i).gameObject;
                break;
            }
        }

        //Get the shield from the children 
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Shield"))
            {
                shield = transform.GetChild(0).gameObject;
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    private void shoot ()
    {

    }
    #endregion

    #region Public Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called by the cardManager to signal the end of the active round
    public void endActiveRound ()
    {
        roundActive = false;

        shieldTime = 0;
        totalThrustPoints = 0;
        timesToShoot = 0;

        forwardThrust = 0;
        backwardThrust = 0;
        leftRotation = 0;
        rightRotation = 0;
        leftThrust = 0;
        rightThrust = 0;

        shootTimer = 0.1f;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by the cardManager when a new active round is starting. Sets up variables to use by the player
    public void applyCards(List<int> chosenCards,int[] cardsInDeck, GameObject forceCanvas, int lvl1Thrust, int lvl2Thrust, int lvl3Thrust, int factor, bool allThrustNeeded, float oneShieldTime)
    {
        roundActive = true;

        //Save provided references
        applyForceCanvas = forceCanvas;
        thrustFactor = factor;
        cardsArray = cardsInDeck;

        //Check each card chosen
        foreach (int cardIndex in chosenCards)
        {
            //Compare to card type in the cards array
            switch (cardsInDeck[cardIndex])
            {
                //Add a bullet to the total
                case 1:
                    {
                        timesToShoot++;
                        break;
                    }
                //Add shield duration
                case 2:
                    {
                        shieldTime += oneShieldTime;
                        break;
                    }
                //Add level 1 thrust points
                case 3:
                    {
                        totalThrustPoints += lvl1Thrust;
                        break;
                    }
                //Add level 2 thrust points
                case 4:
                    {
                        totalThrustPoints += lvl2Thrust;
                        break;
                    }
                //Add level 3 thrust points
                case 5:
                    {
                        totalThrustPoints += lvl3Thrust;
                        break;
                    }
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by the Confirm Thrust button on the applyThrust Canvas
    public void startRound ()
    {

    }
    #endregion
}
