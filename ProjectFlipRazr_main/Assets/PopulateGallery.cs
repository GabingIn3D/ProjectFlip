using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopulateGallery : MonoBehaviour
{
    public Transform gridParent; // Reference to the UI Grid parent
    public string resourcePath = "/Resources"; // Path to the Resources folder containing .png files
    public GameObject imagePrefab; // Prefab for displaying sprites

    private void Start()
    {
        // Load all .png files from the Resources folder
        Sprite[] sprites = Resources.LoadAll<Sprite>(Application.dataPath + resourcePath);

        // Create UI Image objects for each sprite and add them to the grid
        foreach (Sprite sprite in sprites)
        {
            GameObject imageObject = Instantiate(imagePrefab, gridParent);
            Image image = imageObject.GetComponent<Image>();
            image.sprite = sprite;
        }
    }
}
