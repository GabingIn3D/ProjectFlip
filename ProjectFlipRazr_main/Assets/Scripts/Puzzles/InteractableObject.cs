using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{
    public enum Object
    {
        Door,
        AccessToStudio,
    }

    public Object obj;
    public GameObject player;
    public PhotoInfoDatabase photoInfoDatabase;

    private DefaultControls controls;
    private InputAction confirmAction;

    // Start is called before the first frame update
    void Start()
    {
        controls = new DefaultControls();

        // Assuming "PhoneNavigation" is the name of your action map
        InputActionMap phoneNavigationMap = controls.PhoneNavigation;

        if (phoneNavigationMap != null)
        {
            // Assuming "Confirm" is the name of your action within that map
            confirmAction = phoneNavigationMap.FindAction("Confirm");

            if (confirmAction != null)
            {
                // Subscribe to the button press event
                confirmAction.started += ctx => Interact();
            }
            else
            {
                Debug.LogError("Confirm action not found!");
            }
        }
        else
        {
            Debug.LogError("PhoneNavigation action map not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 2f)
        {
            controls.Enable();
        }
        else { controls.Disable(); }
    }

    public void Interact()
    {
        Debug.Log("Interacted");
        switch (obj)
        {
            case Object.Door:
                foreach (PhotoInfo info in photoInfoDatabase.photos)
                {
                    if (info.photoItems.Length > 0)
                    {
                        for (int i = 0; i < info.photoItems.Length; i++)
                        {
                            if (info.photoItems[i] == PhotoInfo.PhotoItem.OfficeKey)
                            {
                                gameObject.GetComponent<LockedDoor>().Unlock();
                                break;
                            }
                        }
                    }
                }
                break;
            case Object.AccessToStudio:
                GlobalPlaytestSettings.instance.hasStudio = true;
                Destroy(gameObject);
                break;
        }
    }
}
