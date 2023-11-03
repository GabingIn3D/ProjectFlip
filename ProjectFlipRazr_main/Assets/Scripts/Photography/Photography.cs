using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Photography : MonoBehaviour
{
    Camera snapCam;
    CinemachineVirtualCamera photographyCamera;

    int resWidth = 1600; // 2 megapixels (Razr 2 v9x photo quality)
    int resHeight = 1200;  // 2 megapixels (Razr 2 v9x photo quality)

    void Awake()
    {
        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        }
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallTakeSnapshot()
    {
        snapCam.gameObject.SetActive(true);
    }

    public void LateUpdate() // we want the photo to be taken after the other stuff (updates) are done (the camera becomes active)
    {
        if (snapCam.gameObject.activeInHierarchy) // if 'snapCam' and all of its parents are on, we can take a picture
        {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false); // RGB 24 is the RGB colour depth, we like that. the final argument is mipmapping which is 'false')
            snapCam.Render();
            RenderTexture.active = snapCam.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0); // controls the space captured by the snapped pic
            byte[] bytes = snapshot.EncodeToPNG();
            string fileName = SnapshotName(); // naming the output PNG
            System.IO.File.WriteAllBytes(fileName, bytes);
            Debug.Log("Snapshot taken! Uwu");
            snapCam.gameObject.SetActive(false);
        }
    }

    string SnapshotName() // decides naming rules for output PNG
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.dataPath, // might want to change this to persistent data path when building
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
