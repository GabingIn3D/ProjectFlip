using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using TMPro;

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

    public int currentPage = 1;
    int maxSlots = 6;



    public GameObject nextPageButton;
    public GameObject previousPageButton;

    public TextMeshProUGUI pageNumber;

    bool nextPageActive;
    bool previousPageActive;



    public void Start()
    {

        photoDatabase = FindAnyObjectByType<PhotoInfoDatabase>();
        switch (whichPath)
        {
            case WhichPath.Editor:

                textures = Resources.LoadAll<Texture2D>(resourcePath);

                int slotNumber = 0;

                pageNumber.text = ("Page" + (currentPage.ToString()));

                // Calculate the slot number
                for (int i = 0; i <= (maxSlots) * currentPage; i++)
                {
                    slotNumber = i - maxSlots;
                }
                
                int amountOfSlots = slotNumber;

                for (int i = slotNumber; i < textures.Length; i++)
                {
                    amountOfSlots += 1;

                    if (amountOfSlots > (maxSlots * currentPage))
                    {
                        // Perhaps check the count of textures[] to ensure that this nextPageButton should exist, so that at least one texture would appear on the next page.
                        nextPageButton.SetActive(true);
                        nextPageActive = true;
                        break;
                    }

                    if (textures[i] == null) break;
                    
                    Sprite sprite = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), Vector2.one * 0.5f);

                    // Create UI Image object and add it to the grid
                    GameObject imageObject = Instantiate(imagePrefab, gridParent);
                    Image image = imageObject.GetComponent<Image>();
                    image.sprite = sprite;

                }

                break;


            case WhichPath.Build:
                textures = new Texture2D[photoDatabase.photoMemoryCount];
                RefreshGallery();
                ShowImagesInAlbum();
                break;
        }


    }

    // Since at the moment I cannot click on buttons, I added these as temporary ways to change pages.
    private void Update()
    {
        if (nextPageActive)
        {
            if (Input.GetKey(KeyCode.RightArrow)) NextPage();
        }
        
        if (previousPageActive)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) PreviousPage();
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
        //        foreach (Texture2D texture in textures)
        //        {
        // Convert Texture2D to Sprite


        int last_el = photoDatabase.photos.Count - 1;
        if (last_el >= 0)
        {
            Sprite sprite = Sprite.Create(textures[last_el], new Rect(0, 0, textures[last_el].width, textures[last_el].height), Vector2.one * 0.5f);

            // Create UI Image object and add it to the grid

            GameObject imageObject = Instantiate(imagePrefab, gridParent);
            Image image = imageObject.GetComponent<Image>();
            image.sprite = sprite;
 
        }

    }


    public void ReloadImagesInGallery()
    {
        textures = new Texture2D[photoDatabase.photos.Count];
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

        ShowImagesInAlbum();
    }






    public void NextPage()
    {
        currentPage += 1;
        ShowImagesInAlbum();
    }

    public void PreviousPage()
    {
        currentPage -= 1;
        ShowImagesInAlbum();
    }

    public void ShowImagesInAlbum()
    {

        pageNumber.text = ("Page" + (currentPage.ToString()));

        // Destroy all from current grid before re-showing only the necessary ones, to simulate a new page.
        for (var i = gridParent.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(gridParent.GetChild(i).gameObject);
        }


        //textures = Resources.LoadAll<Texture2D>(resourcePath);

        int slotNumber = 0;

        if (currentPage > 1)
        {
            previousPageButton.SetActive(true);
            previousPageActive = true;
        }
        else
        {
            previousPageButton.SetActive(false);
            previousPageActive = false;
        } 


        // Calculate the slot number
        for (int i = 0; i <= (maxSlots * currentPage); i++)
        {
            slotNumber = i - maxSlots;
        }

        int amountOfSlots = slotNumber;

        for (int i = slotNumber; i < textures.Length; i++)
        {
            amountOfSlots += 1;

            if (amountOfSlots > (maxSlots * currentPage))
            {
                // Perhaps check the count of textures[] to ensure that this nextPageButton should exist, so that at least one texture would appear on the next page.
                nextPageButton.SetActive(true);
                nextPageActive = true;
                break;
            }

            if (textures[i] == null) break;

            Sprite sprite = Sprite.Create(textures[i], new Rect(0, 0, textures[i].width, textures[i].height), Vector2.one * 0.5f);

            // Create UI Image object and add it to the grid
            GameObject imageObject = Instantiate(imagePrefab, gridParent);
            Image image = imageObject.GetComponent<Image>();
            image.sprite = sprite;

        }

        if (amountOfSlots <= (maxSlots * currentPage))
        {
            nextPageActive = false;
            nextPageButton.SetActive(false);
        }

    }







}
