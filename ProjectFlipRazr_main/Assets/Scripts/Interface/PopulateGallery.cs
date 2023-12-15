using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine.EventSystems;

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
    public TextureHolder textureHolder;
    public PhotoInfo containedPhotoInfo;
    public PhotoInfoDatabase photoDatabase;
    public GalleryListBehaviour galleryListBehaviour;
    public WhichPath whichPath;
    public string imagePath;
    public string snapshotSavePath;

    private GameObject newestPhoto;

    //public int currentPage = 1;
    //int maxSlots = 16;

    //public GameObject nextPageButton;
    //public GameObject previousPageButton;

    //public TextMeshProUGUI pageNumber;

    //bool nextPageActive;
    //bool previousPageActive;



    public void Awake()
    {
        textureHolder = FindAnyObjectByType<TextureHolder>();
        photoDatabase = FindAnyObjectByType<PhotoInfoDatabase>();
        galleryListBehaviour = FindAnyObjectByType<GalleryListBehaviour>();
        ShowImagesInAlbum();
        switch (whichPath)
        {
            case WhichPath.Editor:

                textureHolder.textures = Resources.LoadAll<Texture2D>(resourcePath);

                break;


            case WhichPath.Build:

                //int slotNumber = 0;

                //pageNumber.text = ("Page" + (currentPage.ToString()));

                //// Calculate the slot number
                //for (int i = 0; i <= (maxSlots) * currentPage; i++)
                //{
                //    slotNumber = i - maxSlots;
                //}

                //int amountOfSlots = slotNumber;

                //for (int i = slotNumber; i < textureHolder.textures.Length; i++)
                //{
                //    amountOfSlots += 1;

                //    if (amountOfSlots > (maxSlots * currentPage))
                //    {
                //        // Perhaps check the count of textureHolder.textures[] to ensure that this nextPageButton should exist, so that at least one texture would appear on the next page.
                //        nextPageButton.SetActive(true);
                //        nextPageActive = true;
                //        break;
                //    }
                //}

                break;
        }


    }

    // Since at the moment I cannot click on buttons, I added these as temporary ways to change pages.
    private void Update()
    {
        //if (nextPageActive)
        //{
        //    if (Input.GetKey(KeyCode.RightArrow)) NextPage();
        //}

        //if (previousPageActive)
        //{
        //    if (Input.GetKey(KeyCode.LeftArrow)) PreviousPage();
        //}
    }

    //public void NextPage()
    //{
    //    currentPage += 1;
    //    ShowImagesInAlbum();
    //}

    //public void PreviousPage()
    //{
    //    currentPage -= 1;
    //    ShowImagesInAlbum();
    //}

    public void ShowImagesInAlbum()
    {
        //Debug.Log("MaxSlots is " + maxSlots + ", ");
        //pageNumber.text = ("Page" + (currentPage.ToString()));

        //// Destroy all from current grid before re-showing only the necessary ones, to simulate a new page.
        //for (var i = gridParent.childCount - 1; i >= 0; i--)
        //{
        //    Object.Destroy(gridParent.GetChild(i).gameObject);
        //}

        if (whichPath == WhichPath.Editor)
            textureHolder.textures = Resources.LoadAll<Texture2D>(resourcePath);
        // else textureHolder.textures = new Texture2D[photoDatabase.photoMemoryCount];
        //int endIdx = Mathf.Min(currentPage * maxSlots, textureHolder.textures.Length);


        int startIdx = 0; // this is the first photo in the list of textures from 'textureHolder'.
        int endIdx = textureHolder.textures.Length - 1; // This is total number of photos taken

        for (int i = endIdx; i >= startIdx; i--)
        
        {
            Debug.Log(i);
            Sprite sprite = Sprite.Create(textureHolder.textures[i], new Rect(0, 0, textureHolder.textures[i].width, textureHolder.textures[i].height), Vector2.one * 0.5f);

            // Create UI Image object and add it to the grid
            GameObject imageObject = Instantiate(imagePrefab, gridParent);
            Image image = imageObject.GetComponent<Image>();
            image.sprite = sprite;

            if(i == endIdx)
            {
                newestPhoto = imageObject;
                Debug.Log("PopulateGallery.cs: " + i + ": NewestPhoto is " + photoDatabase.photos[i].photoName + "/" + imageObject.name);
            }

            imageObject.GetComponent<PhotoInfoContainer>().photoName = photoDatabase.photos[i].photoName;
            
            // Create a list to store the item strings
            List<string> itemStrings = new List<string>();

            // Iterate over the photoItems array and get the corresponding strings
            foreach (PhotoInfo.PhotoItem item in photoDatabase.photos[i].photoItems)
            {
                string itemString = photoDatabase.photos[i].GetPhotoItemString(item);
                itemStrings.Add(itemString);
            }
            string concatenatedItems = string.Join(", ", itemStrings);

            if (photoDatabase.photos[i].photoItems.Length >= 3)
            {
                concatenatedItems += ", More...";
            }

            imageObject.GetComponent<PhotoInfoContainer>().photoItems = concatenatedItems;
            imageObject.GetComponent<PhotoInfoContainer>().photoLocation = photoDatabase.photos[i].GetLocationString();
            imageObject.GetComponent<PhotoInfoContainer>().photoTime = photoDatabase.photos[i].photoTime.ToString();

            ///WE NEED TO REMAKE THE BUTTON SYSTEM RN


            //PhotoStickyButton photoStickyButton = imageObject.AddComponent<PhotoStickyButton>();

            //photoStickyButton.containedPhotoInfo = photoDatabase.photos[i];

            //    // Assuming the GameObjects are named "GalleryHoleOfficial1" to "GalleryHoleOfficial16"
            //    string gameObjectName = $"GalleryHoleOfficial{i + 1}";

            //    // Assuming ListTargetEntries is a Dictionary<string, GameObject>
            //    if (galleryListBehaviour.ListTargetEntries.TryGetValue(gameObjectName, out GameObject gameObject))
            //    {
            //        // Get the PhotoStickyButton component from the GameObject
            //        PhotoStickyButton photoStickyButton = gameObject.GetComponent<PhotoStickyButton>();

            //        if (photoStickyButton != null)
            //        {
            //            photoStickyButton.containedPhotoInfo = photoDatabase.photos[i];
            //            photoStickyButton.MakeInteractable();
            //        }
            //        else
            //        {
            //            Debug.LogError($"Could not find PhotoStickyButton component on {gameObjectName}.");
            //        }
            //    }
            //    else
            //    {
            //        Debug.LogError($"Could not find {gameObjectName} in ListTargetEntries.");
            //    }
            //}


            //for (int i = startIdx; i < endIdx; i++)
            //{
            //    Sprite sprite = Sprite.Create(textureHolder.textures[i], new Rect(0, 0, textureHolder.textures[i].width, textureHolder.textures[i].height), Vector2.one * 0.5f);

            //    // Create UI Image object and add it to the grid
            //    GameObject imageObject = Instantiate(imagePrefab, gridParent);
            //    Image image = imageObject.GetComponent<Image>();
            //    image.sprite = sprite;

            //    // the new thing
            //   galleryListBehaviour.ListTargetEntries
            //        //take photoinfodatabase.photos[i] 
            //        //ListTargetEntries: get component PhotoStickyButton and PhotoStickyButton.containedPhotoInfo
            //        //photoStickyButton.containedPhotoInfo = photoInfoDatabase.photos[i];
            //        //
            //}

            //previousPageActive = currentPage > 1;
            //nextPageActive = endIdx < textureHolder.textures.Length; // Check if there are more images
            //nextPageButton.SetActive(nextPageActive);
            //previousPageButton.SetActive(previousPageActive);
        }

        //ASSIGN THE FIRST SELECTABLE PHOTO TO THE EVENT SYSTE
        if(newestPhoto != null)
        {
            EventSystem.current.SetSelectedGameObject(newestPhoto);
            Debug.Log(newestPhoto.gameObject.name + " is becoming the first selectable through the " + EventSystem.current);
        }
        //TODO: 
        //You are to make a new version of 'PhotoStickyButton.cs' but it doesn't need to be sticky, it just needs to open the Options Context Menu and bring the focus over there.
        //The back button should ideally close the OptionsContextMenu and then have the photo in the gallery that was selected... still selected. If that's too hard, try to make it
        //so at the very least the gallery reloads.

    }
}
