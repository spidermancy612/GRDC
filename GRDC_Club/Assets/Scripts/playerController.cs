using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class playerController : MonoBehaviour {

    /*
     * TODO
     * - Public methods for updating thrust to apply
     * - Canvas for the thrust to be applied
     *      Thinking of having each button press update thrust vars and then a quick check when the press confirm
     *      Don't forget to check the allThrustNeeded boolean
     * - Make sure to plug in health code
     * - I was thinking of raycasting to show the player the ship direction
     */

    #region Instance Variables

    //General Variables///////////////////////////////////////
    private int[] cardsArray;

    private GameObject shootPoint;                          // Location to spawn player projectile when firing
    private GameObject shield;                              // Player shield object
    private GameObject applyForceCanvas;                    // Canvas used to allow the player to apply force to their ship this turn
    private GameObject bullet;

    private Rigidbody playerRigidBody;                      // Rigidbody component on the player

    private int totalThrustPoints;                          // Sum of all thrust available
    private int thrustFactor;                               // Factor to multiply thrust by
    private int timesToShoot;                               // How many times the player will shoot

    private float shieldTime;                               // How long the shield will be active for
    private float bulletSpeed;
    private float shootTimer;

    private float leftThrust;
    private float rightThrust;
    private float forwardThrust;
    private float backwardThrust;
    private float leftRotation;
    private float rightRotation;

    private bool allThrustNeeded;                           // Determines if all thrust must be applied by the player to start the active turn
    private bool roundActive;                               // Determines if the round is in an active state

    private Text totalThrustPointsText;
    private Text forwardThrustText;
    private Text backwardThrustText;
    private Text rightThrustText;
    private Text leftThrustText;
    private Text rightRotationText;
    private Text leftRotationText;
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

        totalThrustPointsText = applyForceCanvas.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        forwardThrustText= applyForceCanvas.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        backwardThrustText = applyForceCanvas.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        rightRotationText = applyForceCanvas.transform.GetChild(3).GetChild(0).GetComponent<Text>();
        leftRotationText = applyForceCanvas.transform.GetChild(4).GetChild(0).GetComponent<Text>();
        rightRotationText = applyForceCanvas.transform.GetChild(5).GetChild(0).GetComponent<Text>();
        leftRotationText = applyForceCanvas.transform.GetChild(6).GetChild(0).GetComponent<Text>();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called every frame to do state checking
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

        //Check if the shield time is greater than 0
        if (shieldTime > 0)
        {
            //Decriment the timer
            shieldTime -= Time.deltaTime;
        }
        else
        {
            //Otherwise turn the shield off
            shield.SetActive(false);
        }
    }
    #endregion

    #region Private Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Find the shootPoint and Shield Gameobjects on the player
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
    //Method called by the update method when a bullet needs to be fired
    private void shoot ()
    {
        GameObject projectileClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        projectileClone.GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletSpeed);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called when the confirm button is pressed on the applyThrust Canvas to apply any thrust the player has chosen to use
    private void applyThrust ()
    {
        totalThrustPoints = 0;

        playerRigidBody.AddForce(Vector3.forward * forwardThrust);
        playerRigidBody.AddForce(Vector3.forward * backwardThrust);
        playerRigidBody.AddForce(Vector3.left * leftThrust);
        playerRigidBody.AddForce(Vector3.right * rightThrust);

        playerRigidBody.AddTorque(Vector3.left * leftRotation);
        playerRigidBody.AddTorque(Vector3.right * rightRotation);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Turns on the shield when the player has confirmed the round to start
    private void enableShield ()
    {
        shield.SetActive(true);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called to update the applyThrust canvas GUI whenever a button is pressed
    private void updateApplyThrustGUI ()
    {
        totalThrustPointsText.text = totalThrustPoints.ToString();
        forwardThrustText.text = forwardThrust.ToString();
        backwardThrustText.text = backwardThrust.ToString();
        leftThrustText.text = leftThrust.ToString();
        rightThrustText.text = rightThrust.ToString();
        leftRotationText.text = leftRotation.ToString();
        rightRotationText.text = rightRotation.ToString();
    }
    #endregion

    #region Public Methods
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Gets the information for the bullet when shooting
    public void getBulletInfo(GameObject shot, float bSpeed)
    {
        bullet = shot;
        bulletSpeed = bSpeed;
    }

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

        bullet = null;
        bulletSpeed = 0f;
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
        //If the player used too many thrust points don't let them continue
        if (totalThrustPoints < 0)
        {
            return;
        }

        if (allThrustNeeded)
        {
            if (totalThrustPoints == 0)
            {
                applyThrust();
                enableShield();
            }
        }
        else
        {
            applyThrust();
            enableShield();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by buttons on the applyThrust canvas to update the forward thrust
    public void modifyForwardThrust (int updateValue)
    {
        totalThrustPoints += updateValue;
        forwardThrust += updateValue;
        updateApplyThrustGUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by buttons on the applyThrust canvas to update the backward thrust
    public void modifyBackwardThrust (int updateValue)
    {
        totalThrustPoints += updateValue;
        backwardThrust += updateValue;
        updateApplyThrustGUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by buttons on the applyThrust canvas to update the leftThrust
    public void modifyLeftThrust (int updateValue)
    {
        totalThrustPoints += updateValue;
        leftThrust += updateValue;
        updateApplyThrustGUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by buttons on the applyThrust canvas to update the rightThrust
    public void modifyRightThrust (int updateValue)
    {
        totalThrustPoints += updateValue;
        rightThrust += updateValue;
        updateApplyThrustGUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by buttons on the applyThrust canvas to update the leftRotation
    public void modifyLeftRotation (int updateValue)
    {
        totalThrustPoints += updateValue;
        leftRotation += updateValue;
        updateApplyThrustGUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Called by buttons on the applyThrust canvas to update the rightRotation
    public void modifyRightRotation (int updateValue)
    {
        totalThrustPoints += updateValue;
        rightRotation += updateValue;
        updateApplyThrustGUI();
    }
    #endregion
}
