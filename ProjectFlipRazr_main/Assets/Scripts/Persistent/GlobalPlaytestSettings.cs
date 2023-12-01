using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlaytestSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalPlaytestSettings instance;

    [Header("PhoneApps")]
    // PHONE APPS
    public bool hasCamera;
    public bool hasGallery;
    public bool hasMap;
    public bool hasSettings;
    public bool hasSocialMedia;
    public bool hasSaveQuit;

    [Header("Locations")]
    // LOCATIONS
    public bool hasKimmiesHouse;
    public bool hasStudio;
    public bool hasVillas;

    [Header("StudioPhotoItems")]
    public bool hasRingImprint;
    public bool hasOfficeKey;
    public bool hasDirectorPhotoFrame;

    [Header("Public Strings")]
    public string currentLocation;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
