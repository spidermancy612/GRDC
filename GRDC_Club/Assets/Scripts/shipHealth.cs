using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipHealth : MonoBehaviour {

    [Tooltip("Maximum health for the object")]
    public float maxHealth;                     // Max Health the unit can have - set by developer
    [HideInInspector]
    public float damageTaken;                   // Damage to be taken at the end of each frame - set by another script

    private float currentHealth;                // Current health

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called at start of scene
	void Start () {
        //Error logging
        if (maxHealth <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - Max health has not been set properly on " + gameObject.name); }

        currentHealth = maxHealth;                                                                              // Set current health to developer specified max health
	}

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called every frame
    private void Update()
    {
        if (currentHealth <= 0)                                                                                 // Check if the objects health has fallen to 0
        {
            Debug.Log(gameObject.name + " has died, but I don't know what to do with this information");            // The unit died, still need to integrate death systems
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called at the end of every frame
    void LateUpdate () {
        //Check if any other script has given us damage to take
		if (damageTaken > 0)                                                                                    // Check if any damage has been given to this script
        {
            currentHealth = currentHealth - damageTaken;                                                            // Apply damage to current health
            damageTaken = 0;                                                                                        // Reset damage taken to 0 for next frame
        }
	}
}
