using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class CaptureScreen : MonoBehaviour
{
    public static CaptureScreen instance;
    public string filePath;
    public string photoNameVariable;

    public int snapResWidth = 1600;
    public int snapResHeight = 1200;

    private string dateTimeString;

    void Awake()
    {
        instance = this;
    }

    public void Capture()
    {
        StartCoroutine(AsyncCapture());
    }

    IEnumerator AsyncCapture()
    {
        yield return new WaitForEndOfFrame();
        //var rt = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
        var rt = RenderTexture.GetTemporary(snapResWidth, snapResHeight, 0, RenderTextureFormat.ARGB32);
        ScreenCapture.CaptureScreenshotIntoRenderTexture(rt);
        AsyncGPUReadback.Request(rt, 0, TextureFormat.RGBA32, OnCompleteReadback);
        RenderTexture.ReleaseTemporary(rt);
    }

    void OnCompleteReadback(AsyncGPUReadbackRequest asyncGPUReadbackRequest)
    {
        // get screenshot data as nativearray or handle error
        if (asyncGPUReadbackRequest.hasError)
        {
            Debug.LogError("Error Capturing Screenshot: With AsyncGPUReadbackRequest.");
            return;
        }
        var rawData = asyncGPUReadbackRequest.GetData<byte>();
        // Grab screen dimensions
        //var width = Screen.width;
        //var height = Screen.height;
        var width = snapResWidth;
        var height = snapResHeight;
        var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        var processedData = texture.GetRawTextureData<byte>();
        // now flip vertical pixels
        for (int i = 0; i < rawData.Length; i += 4)
        {
            var arrayIndex = i / 4;
            var x = arrayIndex % width;
            var y = arrayIndex / width;
            var flippedY = (height - 1 - y);
            var flippedIndex = x + flippedY * width;
            // flip the data
            processedData[i] = rawData[flippedIndex * 4];
            processedData[i + 1] = rawData[flippedIndex * 4 + 1];
            processedData[i + 2] = rawData[flippedIndex * 4 + 2];
            processedData[i + 3] = rawData[flippedIndex * 4 + 3];
        }
        // create texture and save as png using datetime
        var dateTime = DateTime.Now;
		dateTimeString = photoNameVariable + dateTime.ToString("-yyyy-MM-dd_") + dateTime.Hour + "h-" + dateTime.Minute + "m";
        File.WriteAllBytes(Application.dataPath + filePath + dateTimeString + ".png", ImageConversion.EncodeToPNG(texture));
        //capture the information from (string path) above, send it to the entry in the class for fileLocation/fileName)
        Debug.Log("Capture written! To " + filePath);
        Destroy(texture);
        RecordPhotoInfo();
    }

    public void RecordPhotoInfo() // Adds a PhotoInfo to the list in the database with the info inside
    {
        PhotoInfo photoInfo = new PhotoInfo();

        //Record photoName
        photoInfo.photoName = dateTimeString + ".png";

        //Record fileLocation
        photoInfo.fileLocation = Application.dataPath.ToString() + filePath.ToString();

        //Record gameLocation
        if (SceneManager.GetActiveScene().name == "House") { photoInfo.gameLocation = PhotoInfo.Location.House; }
        else if (SceneManager.GetActiveScene().name == "Warehouse") { photoInfo.gameLocation = PhotoInfo.Location.Warehouse; }
        else { photoInfo.gameLocation = PhotoInfo.Location.Unknown; }

        //Record photoItems
        photoInfo.photoItems = new PhotoInfo.PhotoItem[1];
        photoInfo.photoItems[0] = PhotoInfo.PhotoItem.Knife;

        //Record photoTime
        photoInfo.photoTime = DateTime.Now;

        //Add Photo
        FindAnyObjectByType<PhotoInfoDatabase>().AddPhoto(photoInfo);
        Debug.Log("PhotoInfo added to list");
    }
}
