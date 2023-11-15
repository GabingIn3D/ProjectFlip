using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadronStateManager : MonoBehaviour
{
    Hadron_PhotoInfo_BaseState currentState;

    //States
    HadronPointOfInterest1 PointOfInterest1 = new HadronPointOfInterest1();
    HadronPointOfInterest2 PointOfInterest2 = new HadronPointOfInterest2();
    HadronPointOfInterest3 PointOfInterest3 = new HadronPointOfInterest3();

    void Start()
    {
        // starting state for the state machine
        currentState = PointOfInterest1;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
