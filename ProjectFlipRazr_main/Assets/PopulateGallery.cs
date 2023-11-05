using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopulateGallery : MonoBehaviour
{
    public Transform gridParent; // UI Grid parent
    public string resourcePath; // Folder containing PNGs
    public GameObject imagePrefab; // Prefab for displaying sprites

    public void Start()
    {

        Texture2D[] textures = Resources.LoadAll<Texture2D>(resourcePath);

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
