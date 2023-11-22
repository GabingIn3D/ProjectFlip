using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPhone_MapState : FlipPhone_BaseState
{
    public string pageBelongingToState = "Map";

    public override void EnterState(FlipPhoneManager flipPhone)
    {
        IEnumerable<GameObject> objectsExceptOne = flipPhone.GetObjectsExceptOne(pageBelongingToState);
        foreach (GameObject obj in objectsExceptOne)
        {
            obj.SetActive(false);
        }
        flipPhone.options.SetActive(false);
    }

    public override void UpdateState(FlipPhoneManager flipPhone)
    {
        throw new System.NotImplementedException();
    }
}