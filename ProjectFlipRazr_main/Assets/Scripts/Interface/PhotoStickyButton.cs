using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PhotoStickyButton : MonoBehaviour
{
    bool selected = false;
    bool alreadySelected = false;
    public Button btn;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.grey;
    public GalleryListBehaviour targetBehaviour;
    public OptionsContextMenu optionsMenu;
    public PhotoInfo containedPhotoInfo;



    [Header("Highlighted Photo")]
    public TextMeshProUGUI picTitle_txt;
    private FlipPhoneManager flipPhone;
    // Define three different colors
    Color nothingSelected = new Color(0.75f, 0.75f, 0.75f, 1f);      // Grey
    Color importantColour = new Color(1f, 0.45f, 0f, 1f);    // Red/Orange
    Color uncategorizedColour = new Color(1f, 1f, 1f, 1f);     // White

    private ColorBlock colors;

    public enum whatTypeOfListTarget
    {
        Location,
        Photo,
        Settings,
        SaveQuit
    }
    public whatTypeOfListTarget optionType;

    public void Awake()
    {
        picTitle_txt.text = "Select a photo";
        flipPhone = FindObjectOfType<FlipPhoneManager>();
        btn = gameObject.GetComponent<Button>();
        colors = btn.colors;
        optionsMenu = FindObjectOfType<OptionsContextMenu>();
    }

    public void ToggleSelected()
    {
        selected = !selected;
        if (alreadySelected)
        {
            picTitle_txt.color = nothingSelected;
            picTitle_txt.text = "Select a photo";
            // makes all buttons selectable again after clicking the already-selected button
            if(targetBehaviour != null)
            {
                targetBehaviour.ResetGameObjectBehaviour();
                targetBehaviour.currentlySelected = null;
            }
            else
            {
                Debug.Log("targetBehaviour is null");
            }

            optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Nothing;

            OptionScreenToggle();

        }

        if (selected)
        {
            Debug.Log("Name: " + containedPhotoInfo.photoName + ", Location: " + containedPhotoInfo.gameLocation + ", Items: " + containedPhotoInfo.photoItems);
            if(containedPhotoInfo.photoItems.Length != 0)
            {
                //Make the text the "important colour"
                picTitle_txt.color = importantColour;
                //if there is an item in the photo, change the 'Highlighted Photo' text to the name of Item(s) contained in it
                picTitle_txt.text = containedPhotoInfo.photoItems[0].ToString();
                if (containedPhotoInfo.photoItems.Length > 1)
                {
                    picTitle_txt.text = containedPhotoInfo.photoItems[1].ToString() + ", " + containedPhotoInfo.photoItems[0].ToString();
                }
                if (containedPhotoInfo.photoItems.Length >= 2)
                {
                    picTitle_txt.text = containedPhotoInfo.photoItems[2].ToString() + ", " + containedPhotoInfo.photoItems[1].ToString() + ", More...";
                }
            }
            else
            {
                //make the text a different colour to distinguish that it's an unimportant photo
                picTitle_txt.color = uncategorizedColour;
                //if there are no items in the photo, change the 'Highlighted Photo' text to the name of the location it was taken in
                picTitle_txt.text = containedPhotoInfo.gameLocation.ToString();
            }
            //var colors = btn.colors;
            colors.normalColor = selectedColor;
            colors.selectedColor = selectedColor;
            btn.colors = colors;

            //Target 'Options' script and give it information regarding this button

            string currentlySelected = gameObject.name;
            if(targetBehaviour != null)
            {
                targetBehaviour.currentlySelected = currentlySelected;
                targetBehaviour.DeselectAllButOne(currentlySelected);
            }
            else
            {
                Debug.Log("targetBehaviour is null");
            }
            alreadySelected = true;
            switch (optionType)
            {
                case whatTypeOfListTarget.Location:
                    optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Location;

                    OptionScreenToggle();

                    //feed location string from this button
                    break;
                case whatTypeOfListTarget.Photo:
                    optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Photo;

                    OptionScreenToggle();
                    //OptionsContextMenu.cs - enable Photo choices
                    //feed photo name string/identifier
                    break;
                case whatTypeOfListTarget.Settings:
                    optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.Settings;

                    OptionScreenToggle();
                    //if(correspondingSettingID != null)
                    //  tell OptionsContextMenu.cs what setting ID this list item is supposed to be from GlobalPlaytestSettings.cs script
                    //  in MasterSettings script define what options should appear/what they do for each setting ID
                    break;
                case whatTypeOfListTarget.SaveQuit:
                    optionsMenu.currentButtonType = OptionsContextMenu.ButtonType.SaveQuit;

                    OptionScreenToggle();
                    //options menu enable SaveQuit choices
                    //
                    break;
            }

        }
        else
        {
            //var colors = btn.colors;
            colors.normalColor = normalColor;
            colors.selectedColor = normalColor;
            btn.colors = colors;
        }
    }
    
    void OptionScreenToggle()
    {
        optionsMenu.SetButtonsVisibility();
        flipPhone.options.SetActive(false);
        //flipPhone.options.SetActive(true);
    }

}