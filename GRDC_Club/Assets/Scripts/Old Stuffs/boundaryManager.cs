using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaryManager : MonoBehaviour {

    public float playspaceSize;                 // Width and Height of the playspace
    [Space(10)]
    public string asteriodTag;                  // Tag for asteroids
    public string shipTag;                      // Tag for ships
    public string projectileTag;                // Tag for projectiles

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Method called when the object is created or activated
    void Awake () {
        setupRightWall();
        setupLeftWall();
        setupTopWall();
        setupBottomWall();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Create the right wall
    private void setupRightWall ()
    {
        GameObject boundaryRight = GameObject.CreatePrimitive(PrimitiveType.Cube);                              // Create right boundary object
        boundaryRight.transform.position = transform.right * playspaceSize / 2;                                 // Set the position of the object
        boundaryRight.transform.localScale = new Vector3(1, 1, playspaceSize);                                  // Scale object to size
        BoxCollider boxR = boundaryRight.AddComponent(typeof(BoxCollider)) as BoxCollider;                      // Add a box collider to object
        boxR.isTrigger = true;                                                                                  // Set isTrigger to true
        boundaryRight.AddComponent<playspaceWallManager>();                                                     // Add playspaceWallManager script
        boundaryRight.GetComponent<playspaceWallManager>().updateTags(shipTag,asteriodTag,projectileTag);       // Send the tags to the wall
        
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Create the left wall
    private void setupLeftWall ()
    {
        GameObject boundaryLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);                               // Create left boundary object
        boundaryLeft.transform.position = transform.right * playspaceSize / 2 * -1;                             // Set the position of the object
        boundaryLeft.transform.localScale = new Vector3(1, 1, playspaceSize);                                   // Set the scale of the object
        BoxCollider boxL = boundaryLeft.AddComponent(typeof(BoxCollider)) as BoxCollider;                       // Add a box collider 
        boxL.isTrigger = true;                                                                                  // Set the box collider to a trigger
        boundaryLeft.AddComponent<playspaceWallManager>();                                                      // Add playspaceWallManager script
        boundaryLeft.GetComponent<playspaceWallManager>().updateTags(shipTag, asteriodTag, projectileTag);      // Send the tags to the walls
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Create the top wall
    private void setupTopWall ()
    {
        GameObject boundaryTop = GameObject.CreatePrimitive(PrimitiveType.Cube);                                // Creae the top boundary object
        boundaryTop.transform.position = transform.forward * playspaceSize / 2;                                 // Set the position of the object
        boundaryTop.transform.localScale = new Vector3(playspaceSize, 1, 1);                                    // Scale the object
        BoxCollider boxT = boundaryTop.AddComponent(typeof(BoxCollider)) as BoxCollider;                        // Attach a box collider
        boxT.isTrigger = true;                                                                                  // Set the box collider to a trigger
        boundaryTop.AddComponent<playspaceWallManager>();                                                       // Attach playspaceWallManager to the wall
        boundaryTop.GetComponent<playspaceWallManager>().updateTags(shipTag, asteriodTag, projectileTag);       // Update the tags on the wall
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Create the bottom wall
    private void setupBottomWall ()
    {
        GameObject boundaryBottom = GameObject.CreatePrimitive(PrimitiveType.Cube);                             // Create the bottom boundary object
        boundaryBottom.transform.position = transform.forward * playspaceSize / 2 * -1;                         // Set the position of the object
        boundaryBottom.transform.localScale = new Vector3(playspaceSize, 1, 1);                                 // Scale the object
        BoxCollider boxB = boundaryBottom.AddComponent(typeof(BoxCollider)) as BoxCollider;                     // Attach a box collider
        boxB.isTrigger = true;                                                                                  // Set the collider to a trigger
        boundaryBottom.AddComponent<playspaceWallManager>();                                                    // Attach a playspaceWallManager to the object
        boundaryBottom.GetComponent<playspaceWallManager>().updateTags(shipTag, asteriodTag, projectileTag);    // Update the tags on the wall
    }
}
