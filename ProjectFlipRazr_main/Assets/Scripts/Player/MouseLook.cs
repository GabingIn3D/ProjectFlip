using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 8f;
    [SerializeField] float sensitivityY = 0.5f;
    float cameraX, cameraY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, cameraX * Time.deltaTime);
    }

    public void ReceiveInput (Vector2 lookInput)
    {
        cameraX = lookInput.x * sensitivityX;
        cameraY = lookInput.y * sensitivityY;
    }
}
