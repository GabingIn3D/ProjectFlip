using System.Collections;
using System.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public float delayBeforePrinting;
    public DialogueStartHere dialogueSystem;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage; // Assuming this is the Image component for the Sprite
    public bool manualClick;
    public float fadeInTime = 1f;
    public float fadeOutTime = 1f;

    private DefaultControls controls;
    private InputAction confirmAction;

    void Start()
    {
        controls = new DefaultControls();

        // Assuming "PhoneNavigation" is the name of your action map
        InputActionMap phoneNavigationMap = controls.PhoneNavigation;

        if (phoneNavigationMap != null)
        {
            // Assuming "Confirm" is the name of your action within that map
            InputAction confirmAction = phoneNavigationMap.FindAction("Confirm");

            if (confirmAction != null)
            {
                // Subscribe to the button press event
                confirmAction.started += ctx => OnConfirmButtonPressed();
            }
            else
            {
                Debug.LogError("Confirm action not found!");
            }
        }
        else
        {
            Debug.LogError("PhoneNavigation action map not found!");
        }

        controls.Enable();
        StartCoroutine(DisplayDialogue());
    }

    IEnumerator DisplayDialogue()
    {
        yield return new WaitForSeconds(delayBeforePrinting);


        var actionMap = new UnityEngine.InputSystem.InputActionMap("PhoneNavigation");
        confirmAction = actionMap.FindAction("Confirm");

        for (int i = 0; i < dialogueSystem.GetDialogueLineCount(); i++)
        {
            DialogueStartHere.DialogueLine dialogueLine = dialogueSystem.GetDialogueLine(i);

            // Load and set the Sprite for portrait
            if (dialogueLine.portrait != null)
            {
                dialogueImage.sprite = dialogueLine.portrait;
            }

            // Set UI sprite alpha to 0
            Color spriteColor = dialogueImage.color;
            spriteColor.a = 0f;
            dialogueImage.color = spriteColor;

            // Set UI text
            dialogueText.text = $"{dialogueLine.speakerName}: {dialogueLine.text}";


            Debug.Log("manual click is received as " + dialogueLine.manualClick);

            // Fade in TextMeshPro and Sprite
            float elapsedTime = 0f;
            while (elapsedTime < fadeInTime)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInTime);

                // Set UI text alpha
                Color textColor = dialogueText.color;
                textColor.a = alpha;
                dialogueText.color = textColor;

                // Set UI sprite alpha
                spriteColor.a = alpha;
                dialogueImage.color = spriteColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Debug.Log("alpha is " + spriteColor.a);

            // Wait for the specified duration
            yield return new WaitForSeconds(dialogueLine.durationInSeconds);
            // Detect whether this line will require a manual click from the scriptable object's declaration
            manualClick = dialogueLine.manualClick;
            // If "Manual Click" book is true, you will have to press the Confirm button to move to the next line.
            if (manualClick)
            {
                yield return new WaitUntil(() => confirmAction.triggered);

                // Code here will execute after the Confirm button is pressed
                Debug.Log("Confirm button pressed. Continuing...");
            }

            // Fade out TextMeshPro and Sprite
            elapsedTime = 0f;
            while (elapsedTime < fadeOutTime)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);

                // Set UI text alpha
                Color textColor = dialogueText.color;
                textColor.a = alpha;
                dialogueText.color = textColor;

                // Set UI sprite alpha
                spriteColor.a = alpha;
                dialogueImage.color = spriteColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Debug.Log("alpha is " + spriteColor.a);

            // Clear the UI text
            dialogueText.text = "";

            // Hide the text after the specified hide delay or default to 1 second
            float hideDelay = (dialogueLine.hideDelay > 0) ? dialogueLine.hideDelay : 1f;
            yield return new WaitForSeconds(hideDelay);
        }

        // Clear the UI text after all dialogue lines
        ClearDialogue();
    }


    public void ClearDialogue()
    {
        // Set UI sprite alpha
        Color spriteColor = dialogueImage.color;
        dialogueImage.color = spriteColor;
        spriteColor.a = 0;
        //wipe text
        dialogueText.text = "";
    }

    void OnConfirmButtonPressed()
    {
        Debug.Log("Confirm button pressed!");
    }

}