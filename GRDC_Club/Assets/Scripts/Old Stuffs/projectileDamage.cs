using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   // Make sure a RigidBody on this object for it to move

public class projectileDamage : MonoBehaviour {

    [Tooltip("Damage projectile applies")]
    public float damage;                                // Damage the projectile does when it impacts an object with a shipHealth
    [Tooltip("Tag on objects to apply damage to")]
    public string theTag;                               // Tag to be looking for on object that enters the trigger

    private Rigidbody rb;                               // Reference to Rigidbody component   

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called at the start of the scene
    void Start () {
        //Error logging
		if (damage <= 0) { Debug.LogError("DEVELOPER ERROR - Bad Variable - Damage has not been properly set on " + gameObject.name); }
        if (tag.Equals("")) { Debug.LogError("DEVELOPER ERROR  - Null Variable - Tag has not been set by on the " + gameObject.name + " object"); }

        rb = GetComponent<Rigidbody>();                                                                             // Set reference for Rigidbody component
	}

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called when a collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == theTag)                                                                         // Check if the collider entering the trigger has the tagwe're looking for
        {
            other.GetComponent<shipHealth>().damageTaken += damage;                                                     // Apply damage to object we hit
            Destroy(this.gameObject);                                                                                   // Destroy this projectile
        }
    }
}
