using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneSwitcher : MonoBehaviour
{
    public bool isFirstPersonMode;

    [Header("First Person Mode")]
    public GameObject firstPersonPlayer;
    public InputSystemFirstPersonCharacter firstPController;
    public CinemachineVirtualCamera firstPCam;

    [Header("Third Person Mode")]
    public GameObject kimmieReidModel;
    public PlayerControl thirdPController;

    public Camera mainCamera;

    private FlipPhoneManager flipManager;

    private LayerMask currentLayerMask;
    private LayerMask phoneLayerToHide = (1 << 6);
    private LayerMask onlyVisibleOnPhone = (1 << 8);
    private LayerMask onlyVisibleFirstPerson = (1 << 14);

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.cullingMask &= ~phoneLayerToHide;
        mainCamera.cullingMask &= ~onlyVisibleOnPhone;
        mainCamera.cullingMask &= ~onlyVisibleFirstPerson;

        flipManager = FindAnyObjectByType<FlipPhoneManager>();

        //Start as third person controller
        //firstPersonPlayer.SetActive(false);
        thirdPController.enabled = true;
        kimmieReidModel.SetActive(true);
        firstPController.enabled = false;
        firstPCam.Priority = 9;
        isFirstPersonMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenPhone();
        }
    }

    public void OpenPhone()
    {
        if (isFirstPersonMode == false)
        {
            //Switching to first person controller
            mainCamera.cullingMask |= phoneLayerToHide;
            mainCamera.cullingMask |= onlyVisibleFirstPerson;
            kimmieReidModel.SetActive(false);
            thirdPController.enabled = false;
            firstPController.enabled = true;
            firstPCam.Priority = 16;
            isFirstPersonMode = true;
            FreezeMovement();
            flipManager.SwitchState(flipManager.homeScreenState);
        }
        else if (isFirstPersonMode == true)
        {
            //Switching to third person controller
            mainCamera.cullingMask &= ~phoneLayerToHide;
            mainCamera.cullingMask &= ~onlyVisibleFirstPerson;
            kimmieReidModel.SetActive(true);
            thirdPController.enabled = true;
            firstPController.enabled = false;
            firstPCam.Priority = 9;
            isFirstPersonMode = false;
        }
    }

    public void FreezeMovement()
    {
        if (isFirstPersonMode == false)
        {
            //Freeze third person
            thirdPController.enabled = false;
        }
        else if (isFirstPersonMode == true)
        {
            //Freeze first person
            firstPController.enabled = false;
        }
    }

    public void UnFreezeMovement()
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
}
