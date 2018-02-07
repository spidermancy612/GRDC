using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Script : MonoBehaviour
{
    //Menu states
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject optionsMenu;

    //When it first starts
    void Awake()
    {
        //Always makes the first menu the main menu
        levelMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }



    //When the start button is pressed
    public void OnStartPressed()
    {
        Debug.Log("You started the game!");
    }

    //Navigate to the Level Select Submenu
    public void OnLevelSelect()
    {
        levelMenu.SetActive(true);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    //Navigate to options menu
    public void OnOptionsMenu()
    {
        levelMenu.SetActive(false);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    //Exit the game
    public void OnQuit()
    {
        Application.Quit();
    }

    //Back button
    public void OnBackClick()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    //Level 1 Selected
    public void OnLevelOne()
    {
        Debug.Log("You picked 1");
    }

    //Level 2 Selected
    public void OnLevelTwo()
    {
        Debug.Log("You picked 2");
    }

    //Level 3 Selected
    public void OnLevelThree()
    {
        Debug.Log("You picked 3");
    }

    //Level 4 Selected
    public void OnLevelFour()
    {
        Debug.Log("You picked 4");
    }
}
