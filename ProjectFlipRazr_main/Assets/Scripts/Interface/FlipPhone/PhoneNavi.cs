using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PhoneNavi : MonoBehaviour
{
    [SerializeField]
    private FlipPhoneManager flipPhone;
    // Phone controls:
    // ARROW KEYS (or point and click for now), CONFIRM, CANCEL (BACK)
    //
    // SHORTCUTS
    // "STRAIGHT TO CAMERA" - KEY SHORTCUT THAT GOES STRAIGHT TO THE CAMERA
    // "EXIT PHONE" - KEY SHORTCUT THAT PUTS AWAY PHONE/EXITS FIRST PERSON PHONE VIEW
    
    // BUTTONS
    // Back, Options, Main Menu

    // BACK - RETURNS TO MAIN MENU
    // OPTIONS - BRINGS UP ADDITIONAL CONTEXT OPTIONS, I.E. 'DELETE'
    // MAIN MENU - THE APPS

    // STATES
    // Home Screen, Camera, Apps (Main Menu), Gallery, Photo (Individual), Map (Fast Travel List), Settings
    
    // HOME SCREEN - PHONE BACKGROUND WITH TIME AND TEXT STRING 'HOME SCREEN'
    // --- Navi Buttons: "Options / Main Menu"
    // CAMERA - FOR TAKING PICS AND SHOWS THE VIEWFINDER
    // --- Navi Buttons: "Options / Back"
    // APPS (MAIN MENU) - A GRID OF THE APPS AVAILABLE ON THE PHONE (CAMERA, GALLERY, MAP)
    // --- Navi Buttons: "Options / Back"
    // GALLERY - SHOW PHOTO GRID, SCROLLS FROM TOP TO BOTTOM, NEWEST IMAGES AT TOP.
    // --- Navi Buttons: "Options / Back"
    // PHOTO (INDIVIDUAL) - WHEN THE PLAYER SELECTS A PHOTO FROM THE GALLERY AND LOOKS AT IT + THE METADATA
    // --- Navi Buttons: "Options / Back"
    // MAP (FAST TRAVEL LIST) - FOR NOW, JUST A LIST OF LOCATIONS (by string) WITH A MAP "BACKGROUND" IMAG
    // --- Navi Buttons: "Options / Back"
    // SETTINGS - ANY GAME SETTINGS OR PHONE SETTINGS ON A LIST
    // --- Navi Buttons: "Options / Save"

    // Start is called before the first frame update
    public enum whichNaviButton
    {
        Options,
        MainMenu,
        Back,
        Save
    }
    public whichNaviButton button;

    void Awake()
    {
        var buttontext = GetComponentInChildren<TextMeshProUGUI>();

        switch (button)
        {
            case whichNaviButton.Options:
                buttontext.text = "Options";
                break;
            case whichNaviButton.MainMenu:
                buttontext.text = "Main Menu";
                break;
            case whichNaviButton.Back:
                buttontext.text = "Back";
                break;
            case whichNaviButton.Save:
                buttontext.text = "Save";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickNaviButton(FlipPhoneManager state)
    {
        switch (button)
        {
            case whichNaviButton.Options:
                Debug.Log("you clicked on 'Options'");
                flipPhone.options.SetActive(true);
                break;
            case whichNaviButton.MainMenu:
                Debug.Log("you clicked on 'Main Menu'");
                flipPhone.SwitchState(state.mainMenuState);
                break;
            case whichNaviButton.Back:
                Debug.Log("you clicked on 'Back'");
                //if flipPhone state != mainMenuState
                //      if options is active
                //      { 
                //          hide options;
                //      }
                // else
                // {
                flipPhone.SwitchState(state.homeScreenState);
                break;
            case whichNaviButton.Save:
                Debug.Log("You clicked on 'Save'");
                break;
        }
    }
}
