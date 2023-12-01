using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public PhotoInfoDatabase photoInfoDatabase;

    // Start is called before the first frame update
    void Start()
    {
        photoInfoDatabase = FindAnyObjectByType<PhotoInfoDatabase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TravelToScene(string sceneName)
    {
        photoInfoDatabase.Save();

        if (sceneName == "Studio")
        {
            SceneManager.LoadScene("Studio Warehouse"); // the actual string name of the scene asset
            GlobalPlaytestSettings.instance.currentLocation = "Studio";
        }
        if (sceneName == "House")
        {
            SceneManager.LoadScene("Kimmie's House"); // the actual string name of the scene asset
            GlobalPlaytestSettings.instance.currentLocation = "House";
        }
    }
}
