using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [Header("First Person Mode")]
    public GameObject firstPersonPlayer;
    public InputSystemFirstPersonCharacter firstPController;
    public CinemachineVirtualCamera firstPCam;
    public int defaultFPcameraPriority;
    public bool isFirstPersonMode;

    [Header("Third Person Mode")]
    public MoveInputManager thirdPController;
    public CinemachineFreeLook thirdPCam; // optional for playtesting
    public bool freeLookCamera; // optional for playtesting
    //public CinemachineVirtualCamera lastFixedCamera;

    [Header("Photo Realm")]
    public Transform PhotoRealmToHere;
    public Transform EmptyPhotoRealm;

    private float thirdPPlayerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Start as third person controller
        thirdPController.enabled = true;
        if(freeLookCamera && thirdPCam != null)
        {
            thirdPCam.Priority = 15;
        }
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
            if (PhotoRealmToHere != null)
            {
                firstPersonPlayer.transform.position = PhotoRealmToHere.transform.position;
            }
            else
            {
                firstPersonPlayer.transform.position = EmptyPhotoRealm.transform.position;
            }
            thirdPController.enabled = false;
            thirdPController.movementInput.x = 0;
            thirdPController.movementInput.y = 0;
            if(freeLookCamera && thirdPCam != null) { thirdPCam.Priority = 10; }
            firstPController.enabled = true;
            firstPCam.Priority = 15;
            isFirstPersonMode = true;
        }
        else if (isFirstPersonMode == true)
        {
            //Switching to third person controller
            thirdPController.enabled = true;
            if (freeLookCamera && thirdPCam != null) { thirdPCam.Priority = 15; }
            // if (lastFixedCamera != null && != freeLookCamera) { lastFixedCamera.Priority = 15;}
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
        }
        else if (isFirstPersonMode == true)
        {
            //Unfreeze first person
            firstPController.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotoRealmToHere = other.gameObject.transform.GetChild(0);
    }

    private void OnTriggerExit(Collider other)
    {
        PhotoRealmToHere = null;
    }
}
