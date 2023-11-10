using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlaytestSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalPlaytestSettings instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
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
