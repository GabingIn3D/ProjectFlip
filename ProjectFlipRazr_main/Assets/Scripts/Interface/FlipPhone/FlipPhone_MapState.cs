﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OptionsContextMenu;

public class FlipPhone_MapState : FlipPhone_BaseState
{
    public string pageBelongingToState = "Map";

    public override void EnterState(FlipPhoneManager flipPhone)
    {
        Debug.Log("----------You are in Map State: " + pageBelongingToState + " is active.");
        //Sets every phone page except THIS one to inactive
        IEnumerable<GameObject> objectsExceptOne = flipPhone.GetObjectsExceptOne(pageBelongingToState);
        foreach (GameObject obj in objectsExceptOne)
        {
            obj.SetActive(false);
        }
        ///////
        ///

        /*
        if (flipPhone.options != null)
        {
            var contextMenu = flipPhone.options.GetComponentInChildren<OptionsContextMenu>();
            if (contextMenu != null)
            {
                contextMenu.currentButtonType = ButtonType.Nothing;
            }
            else
            {
                Debug.LogError("OptionsContextMenu component not found on flipPhone.options");
            }
        }
        else
        {
            Debug.LogError("flipPhone.options is null");
        }
        */

        //////////////
        //Gets rid of the Options context menu if it's open
        flipPhone.options.SetActive(false);

        //Sets this phone page as active
        flipPhone.GetObject(pageBelongingToState).SetActive(true);

        flipPhone.NavigationButton_L(PhoneNavi.whichNaviButton.Options);
        flipPhone.NavigationButton_R(PhoneNavi.whichNaviButton.Back);
        // L Button: "Options"
        // R Button: "Back"
    }

    public override void UpdateState(FlipPhoneManager flipPhone)
    {
        flipPhone.currentState = flipPhone.mapState;
    }

    public override void ExitState(FlipPhoneManager flipPhone)
    {
        /*
        if (flipPhone.options != null)
        {
            var contextMenu = flipPhone.options.GetComponentInChildren<OptionsContextMenu>();
            if (contextMenu != null)
            {
                contextMenu.currentButtonType = ButtonType.Nothing;
            }
            else
            {
                Debug.LogError("OptionsContextMenu component not found on flipPhone.options");
            }
        }
        else
        {
            Debug.LogError("flipPhone.options is null");
        }
        */
    }
}