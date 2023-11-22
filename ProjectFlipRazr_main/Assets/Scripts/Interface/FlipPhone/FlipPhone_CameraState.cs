using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlipPhone_CameraState : FlipPhone_BaseState 
{
    public string pageBelongingToState = "PhoneViewfinder";

    public override void EnterState(FlipPhoneManager flipPhone)
    {
        //Sets every phone page except THIS one to inactive
        IEnumerable<GameObject> objectsExceptOne = flipPhone.GetObjectsExceptOne(pageBelongingToState);
        foreach (GameObject obj in objectsExceptOne)
        {
            obj.SetActive(false);
        }

        //Gets rid of the Options context menu if it's open
        flipPhone.options.SetActive(false);

        //Sets this phone page as active
        flipPhone.GetObject(pageBelongingToState).SetActive(true);

        // L Button: "Options"
        // R Button: "Back"
    }

    public override void UpdateState(FlipPhoneManager flipPhone)
    {
        throw new System.NotImplementedException();
    }
}
