using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    //First person mode references
    public InputSystemFirstPersonCharacter firstPController;
    public CinemachineVirtualCamera firstPCam;

    //Third Person mode references
    public MoveInputManager thirdPController;
    public CinemachineFreeLook thirdPCam;

    public bool isFirstPersonMode;

    private float thirdPPlayerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Start as third person controller
        thirdPController.enabled = true;
        thirdPCam.Priority = 15;
        firstPController.enabled = false;
        firstPCam.Priority = 10;
        isFirstPersonMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchControllerMode();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            FreezeMovement();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            UnFreezeMovement();
        }
    }

    void SwitchControllerMode()
    {
        if (isFirstPersonMode == false)
        {
            //Switching to first person controller
            thirdPController.enabled = false;
            thirdPController.movementInput.x = 0;
            thirdPController.movementInput.y = 0;
            thirdPCam.Priority = 10;
            firstPController.enabled = true;
            firstPCam.Priority = 15;
            isFirstPersonMode = true;
        }
        else if (isFirstPersonMode == true)
        {
            //Switching to third person controller
            thirdPController.enabled = true;
            thirdPCam.Priority = 15;
            firstPController.enabled = false;
            firstPCam.Priority = 10;
            isFirstPersonMode = false;
        }
    }

    void FreezeMovement()
    {
        if (isFirstPersonMode == false)
        {
            //Freeze third person
            thirdPController.enabled = false;
            thirdPController.movementInput.x = 0;
            thirdPController.movementInput.y = 0;
        }
        else if (isFirstPersonMode == true)
        {
            //Freeze first person
            firstPController.enabled = false;
        }
    }

    void UnFreezeMovement()
    {
        if (isFirstPersonMode == false)
        {
            //Unfreeze third person
            thirdPController.enabled = true;
            thirdPController.movementInput.x = 0;
            thirdPController.movementInput.y = 0;
        }
        else if (isFirstPersonMode == true)
        {
            //Unfreeze first person
            firstPController.enabled = true;
        }
    }
}
