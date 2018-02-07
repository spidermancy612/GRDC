using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shipHealth : MonoBehaviour {

    [Tooltip("Maximum health for the object")]
    public float maxHealth;                     // Max Health the unit can have - set by developer
    [HideInInspector]
    public float damageTaken;                   // Damage to be taken at the end of each frame - set by another script

    private float currentHealth;                // Current health

    public string sceneOnDeath;                 // The scene to load when the player dies

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called at start of scene
	void Start () {
        //Error logging
        if (maxHealth <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - Max health has not been set properly on " + gameObject.name); }
        if (sceneOnDeath == "") { Debug.LogError("DEVELOPER ERROR - Bad Variable - Scene to load on player death has not been set for " + gameObject.name); }

        currentHealth = maxHealth;                                                                              // Set current health to developer specified max health
	}

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called every frame
    private void Update()
    {
        if (currentHealth <= 0)                                                                                 // Check if the objects health has fallen to 0
        {
            SceneManager.LoadScene(sceneOnDeath);                                                                   // Loads scene specified by developer
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
