using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasSubManager : MonoBehaviour {

    /*
     * Class handles controlling the GUI canvas elements for the player. Tracks what player is inputing data and facilitates the shipSubManager to give
     * control data to the player ship.
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
    [Header("Card Visuals")]
    [SerializeField]
    private GameObject[] cardVisualObjectsArray;
    //Inspector Variables/////////////////////////////////////////////


    //General Variables///////////////////////////////////////////////

    //General Variables///////////////////////////////////////////////
    #endregion


    #region Unity Default Methods

    #endregion


    #region Private Methods

    #endregion


    #region Public Methods

    #endregion
}
