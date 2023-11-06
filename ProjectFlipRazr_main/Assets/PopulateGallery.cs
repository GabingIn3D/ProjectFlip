using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class PopulateGallery : MonoBehaviour
{
    public enum WhichPath
    {
        Editor,
        Build
    }
    public Transform gridParent; // UI Grid parent
    public string resourcePath; // Folder containing PNGs
    public string persistentResourcePath; // Folder containing PNGs (in build version)
    public GameObject imagePrefab; // Prefab for displaying sprites
    public Texture2D[] textures;
    public PhotoInfoDatabase photoDatabase;
    public WhichPath whichPath;
    public string imagePath;
    public string snapshotSavePath;

    public void Start()
    {
        photoDatabase = FindAnyObjectByType<PhotoInfoDatabase>();
        switch (whichPath)
        {
            case WhichPath.Editor:
                textures = Resources.LoadAll<Texture2D>(resourcePath);

                foreach (Texture2D texture in textures)
                {
                    // Convert Texture2D to Sprite
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                    // Create UI Image object and add it to the grid
                    GameObject imageObject = Instantiate(imagePrefab, gridParent);
                    Image image = imageObject.GetComponent<Image>();
                    image.sprite = sprite;
                }
                break;

            case WhichPath.Build:
                textures = new Texture2D[photoDatabase.photoMemoryCount];
                RefreshGallery();
                break;
        }


    }

    public void RefreshGallery()
    {
        textures = new Texture2D[photoDatabase.photoMemoryCount];
        photoDatabase = FindAnyObjectByType<PhotoInfoDatabase>();
        for (int i = 0; i < photoDatabase.photos.Count; i++)
        {
            imagePath = Path.Combine(Application.persistentDataPath, snapshotSavePath, photoDatabase.photos[i].photoName);
            if (File.Exists(imagePath))
            {
                // Load the image as a Texture2D
                byte[] fileData = File.ReadAllBytes(imagePath);
                Texture2D loadedTexture = new Texture2D(128, 128); // Create an empty Texture2D
                loadedTexture.LoadImage(fileData); // Load the image data into the Texture2D
                textures[i] = loadedTexture;
            }
            else
            {
                Debug.LogError("Image file not found: " + imagePath);
                // make a new one
            }
        }
        foreach (Texture2D texture in textures)
        {
            // Convert Texture2D to Sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            // Create UI Image object and add it to the grid
            GameObject imageObject = Instantiate(imagePrefab, gridParent);
            Image image = imageObject.GetComponent<Image>();
            image.sprite = sprite;
        }



    }
}
