using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playspaceWallManager : MonoBehaviour {

    /*
     * Created by Liam Baillie
     * December 5, 2017
     * 
     * Script is attached to the playspace walls by the boundaryManager script found on the GAMEMANAGER.
     * This script handles how to interact with any object in the scene when it enters the trigger for 
     * the wall. 
     */

    [HideInInspector]
    public string shipTag;                  // Tag to check for ship object - informed by boundaryManager
    [HideInInspector]
    public string asteriodTag;              // Tag to check for asteriod objects - informed by boundaryManager
    [HideInInspector]
    public string projectileTag;            // Tag to check for projectile object - informed by boundaryManager

	///////////////////////////////////////////////////////////////////////////////////////////////
    //Method runs on start of scene
	void Start () {
		
	}

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //Method called when a collider enters the trigger volume
    private void OnTriggerEnter(Collider other)
    {
        //Check for ship 
        if (other.gameObject.tag == shipTag)
        {
            shipAction(other.gameObject);
        } 
        //Check for asteriod
        else if (other.gameObject.tag == asteriodTag)
        {
            asteriodAction(other.gameObject);
        }
        //Check for projectile
        else if (other.gameObject.name == projectileTag)
        {
            projectileAction(other.gameObject);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //How to affect the ship object when it enters the trigger
    private void shipAction (GameObject ship)
    {
        float xPos = ship.transform.position.x * -1;
        float yPos = ship.transform.position.y * -1;
        float zPos = ship.transform.position.z * -1;

        ship.transform.position = new Vector3(xPos, yPos, zPos);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //How to affect the asteriod object when it enters the volume
    private void asteriodAction (GameObject asteriod)
    {
        float xPos = asteriod.transform.position.x * -1;
        float yPos = asteriod.transform.position.y * -1;
        float zPos = asteriod.transform.position.z * -1;

        asteriod.transform.position = new Vector3(xPos, yPos, zPos);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //How to affect the projectile when it enters the volume
    private void projectileAction (GameObject projectile)
    {
        Destroy(projectile);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    //
    public void updateTags (string s, string a, string p)
    {
        shipTag = s;
        asteriodTag = a;
        projectileTag = p;
    }
}
