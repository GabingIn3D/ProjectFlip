using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlipPhoneManager : MonoBehaviour
{
    FlipPhone_BaseState currentState;

    //Phone pages
    //public GameObject home;
    //public GameObject gallery;
    //public GameObject photoIndividual;
    //public GameObject appMenu;
    //public GameObject map;
    //public GameObject settings;
    public string[] objectNames = { "Home", "Gallery", "PhotoIndividual", "AppMenu", "Map", "Settings" };

    public Dictionary<string, GameObject> phonePages = new Dictionary<string, GameObject>();

    //"Options" context menu overlay
    public GameObject options;

    //PhoneNavi buttons
    public GameObject lButton;
    public GameObject rButton;

    //States
    public FlipPhone_HomeScreenState homeScreenState = new FlipPhone_HomeScreenState();
    public FlipPhone_CameraState cameraState = new FlipPhone_CameraState();
    public FlipPhone_GalleryState galleryState = new FlipPhone_GalleryState();
    public FlipPhone_PhotoIndividualState photoIndividualState = new FlipPhone_PhotoIndividualState();
    public FlipPhone_MainMenuState mainMenuState = new FlipPhone_MainMenuState();
    public FlipPhone_MapState mapState = new FlipPhone_MapState();
    public FlipPhone_SettingsState settingsState = new FlipPhone_SettingsState();

    void Start()
    {
        //starting state for the state machine
        foreach (string name in objectNames)
        {
            AddObject(name, GameObject.Find(name));
        }
        currentState = homeScreenState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(FlipPhone_BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    // Dictionary business
    private void AddObject(string name, GameObject obj)
    {
        if (!phonePages.ContainsKey(name))
        {
             phonePages.Add(name, obj);
        }
        else
        {
            Debug.LogWarning("Object with the name " + name + " already exists in the dictionary.");
        }
    }

    // Method to get a GameObject by name from the dictionary
    private GameObject GetObject(string name)
    {
        if (phonePages.ContainsKey(name))
        {
            return phonePages[name];
        }
        else
        {
            Debug.LogWarning("Object with the name " + name + " does not exist in the dictionary.");
            return null;
        }
    }

    public IEnumerable<GameObject> GetObjectsExceptOne(string excludedObjectName)
    {
        return phonePages
            .Where(pair => pair.Key != excludedObjectName)
            .Select(pair => pair.Value);
    }
}
