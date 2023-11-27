using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionButtonBehaviour : MonoBehaviour
{
    private TextMeshProUGUI displayedText;
    //Possible buttons
    public enum buttonFunction
    {

        Null,

        //Location
        Travel,

        //Camera

        //Gallery
        Expand,
        Delete,

        //Settings
        EnableDebug,

        // SaveQuit
        SaveAndQuit,
        ExitWithoutSaving,

        //Home Screen
        ChangeWallpaper,

    }
    // public OptionsContextMenu contextMenu;
    public buttonFunction buttonfunction;

    // Start is called before the first frame update

    public void OnEnable()
    {
        Button button = GetComponent<Button>();
        displayedText = GetComponentInChildren<TextMeshProUGUI>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
        SetText();
    }

    public void OnDisable()
    {
            displayedText = null;
    }

    public void OnClick()
    {
        switch (buttonfunction)
        {
            case buttonFunction.Null:
                break;

            //Location
            case buttonFunction.Travel:
                var contextMenu = FindObjectOfType<OptionsContextMenu>();
                string selectedLocation = contextMenu.selectedLocation; 
                if (string.IsNullOrEmpty(selectedLocation))
                {
                    Debug.LogWarning("contextMenu.selectedLocation is undefined");
                    return;
                }
                else
                {
                    //      SceneManager.TravelToScene(selectedLocation);
                    Debug.Log("TRAVEL: You are travelling to " +  selectedLocation);
                }
                break;

            //Photo
            case buttonFunction.Expand:
                //Insert FlipPhoneStateManager reference, change phone state to FlipPhone_PhotoIndividualState
                //      show selected gallery image but bigger + with metadata displayed in text for the player to read
                break;

            case buttonFunction.Delete:
                //Insert FlipPhoneStateManager reference, change phone state to FlipPhone_PhotoIndividualState
                //      show selected gallery image but bigger + with metadata displayed in text for the player to read,
                //      overlayed with "Delete pic? Yes/No" pop-up, then handle accordingly in a DeletePhoto function
                //      which should likely reside in a script that can manage PhotoInfoDatabase.
                break;

            //Settings
            case buttonFunction.EnableDebug:
                //if(!GlobalPlayTestSettings.EnableDebug) {
                //      GlobalPlayTestSettings.EnableDebug == true;
                //      Change displayed text object to indicate it's 'ON'
                //}
                //else {
                //      GlobalPlayTestSettings.EnableDebug == false;
                //      Change displayed text object to indicate it's 'OFF'
                //}
                break;
            //SaveQuit
            case buttonFunction.SaveAndQuit:
                //Run Toma's PhotoInfo saving and return to Title Screen using SceneManager reference
                //      SceneManager.TravelToScene(string sceneName);
                break;
            case buttonFunction.ExitWithoutSaving:
                //Close game application without doing anything
                // WARNING: We will likely need to create a middleman in the game to hold onto the images before writing them to the disk during 'SaveAndQuit'
                break;
            //HomeScreen
            case buttonFunction.ChangeWallpaper:
                //a whole buncha shit to change wallpaper (TBD)
                break;
        }

    }

    public void SetText()
    {
        displayedText = GetComponentInChildren<TextMeshProUGUI>();
        Button button = GetComponent<Button>();
        switch (buttonfunction)
        {

            case buttonFunction.Null:
                displayedText.text = "";
                // this is where you set the button's hoverability to be inactive
                break;

            //Location
            case buttonFunction.Travel:
                displayedText.text = "Travel";
                break;

            //Photo
            case buttonFunction.Expand:
                displayedText.text = "Expand";
                break;

            case buttonFunction.Delete:
                displayedText.text = "Delete";
                break;

            //Settings
            case buttonFunction.EnableDebug:
                displayedText.text = "Enable Debug (N/A)";
                break;

            //SaveQuit
            case buttonFunction.SaveAndQuit:
                displayedText.text = "Confirm";
                break;
            case buttonFunction.ExitWithoutSaving:
                displayedText.text = "Exit Game";
                break;

            //HomeScreen
            case buttonFunction.ChangeWallpaper:
                var homeScreen = FindObjectOfType<HomeScreenBehaviour>();
                if(homeScreen != null)
                {
                    homeScreen.GetComponent<HomeScreenBehaviour>();
                    string stringToDisplay = homeScreen.homeScreenOptions[0];
                    displayedText.text = stringToDisplay;
                }
                else
                {
                    Debug.LogWarning("homeScreenis NULLLLLLL");
                }
                break;
        }
    }
}
