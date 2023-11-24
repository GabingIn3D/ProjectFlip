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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchControllerMode();
        }
    }

    void SwitchControllerMode()
    {
        if (isFirstPersonMode == false)
        {
            thirdPController.enabled = false;
            thirdPCam.Priority = 10;
            firstPController.enabled = true;
            firstPCam.Priority = 15;
            isFirstPersonMode = true;
        }
        else if (isFirstPersonMode == true)
        {
            thirdPController.enabled = true;
            thirdPCam.Priority = 15;
            firstPController.enabled = false;
            firstPCam.Priority = 10;
            isFirstPersonMode = false;
        }
    }
}
