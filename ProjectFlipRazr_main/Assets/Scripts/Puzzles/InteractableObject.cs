using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InteractableObject : MonoBehaviour
{
    public enum Object
    {
        Door,
    }

    public Object obj;
    public GameObject player;
    public PhotoInfoDatabase photoInfoDatabase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 2f)
            {
                Interact();
            }
        }
    }

    public void Interact()
    {
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
        }
    }
}
