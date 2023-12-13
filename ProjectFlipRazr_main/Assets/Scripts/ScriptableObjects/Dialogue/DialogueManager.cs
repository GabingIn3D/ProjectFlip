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
    public RawImage dialogueImage; // Assuming this is the RawImage component for the Sprite
    public bool manualClick;

    // TextMeshPro fade times
    public float textMeshProFadeInTime;
    public float textMeshProFadeOutTime;

    // Sprite fade times
    public float rawImageFadeInTime;
    public float rawImageFadeOutTime;


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
            confirmAction = phoneNavigationMap.FindAction("Confirm");

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

    private void Update()
    {
       
    }

    IEnumerator DisplayDialogue()
    {
        yield return new WaitForSeconds(delayBeforePrinting);

        for (int i = 0; i < dialogueSystem.GetDialogueLineCount(); i++)
        {
            DialogueStartHere.DialogueLine dialogueLine = dialogueSystem.GetDialogueLine(i);

            // Load and set the Sprite for portrait
            if (dialogueLine.portrait != null)
            {
                dialogueImage.texture = dialogueLine.portrait;
            }

            // Set UI Sprite RawImage alpha to 0
            Color rawImageColor = dialogueImage.color;
            //rawImageColor.a = 0f;
            //desiredAlpha = 0f;
            dialogueImage.color = rawImageColor;

            // Set UI text
            dialogueText.text = $"{dialogueLine.speakerName}: {dialogueLine.text}";

            // Fade in TextMeshPro and Sprite independently

            yield return StartCoroutine(FadeIn(dialogueImage, rawImageFadeInTime));
            yield return StartCoroutine(FadeIn(dialogueText, textMeshProFadeInTime));


            // Wait for the specified duration
            yield return new WaitForSeconds(dialogueLine.durationInSeconds);

            // Detect whether this line will require a manual click from the scriptable object's declaration
            manualClick = dialogueLine.manualClick;

            // If "Manual Click" is true, you will have to press the Confirm button to move to the next line.
            if (manualClick)
            {
                if (confirmAction == null)
                {
                    Debug.LogError("confirmAction is null. Manual click won't work!");
                    yield break; // exit the coroutine to avoid further issues
                }

                Debug.Log("Waiting for Confirm button press...");
                yield return new WaitUntil(() => confirmAction.triggered);
                Debug.Log("Confirm button pressed. Continuing...");
            }

            // Fade out TextMeshPro and Sprite independently
            yield return StartCoroutine(FadeOut(dialogueImage, rawImageFadeOutTime));
            yield return StartCoroutine(FadeOut(dialogueText, textMeshProFadeOutTime));


            // Clear the UI text
            dialogueText.text = "";

            // Hide the text after the specified hide delay or default to 1 second
            float hideDelay = (dialogueLine.hideDelay > 0) ? dialogueLine.hideDelay : 1f;
            yield return new WaitForSeconds(hideDelay);
        }

        // Clear the UI text after all dialogue lines
        ClearDialogue();
    }

    IEnumerator FadeIn(Graphic graphic, float fadeInTime)
    {
        float elapsedTime = 0f;
        Color startColor = graphic.color;
        float targetAlpha = 1f; // Set the target alpha

        while (startColor.a < targetAlpha)
        {
            float alpha = Mathf.Lerp(0f, targetAlpha, elapsedTime / fadeInTime);
            startColor.a = alpha;
            graphic.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeIn(RawImage rawImage, float fadeInTime)
    {
        float elapsedTime = 0f;
        Color startColor = rawImage.color;
        float targetAlpha = 1f; // Set the target alpha

        while (startColor.a < targetAlpha)
        {
            float alpha = Mathf.Lerp(0f, targetAlpha, elapsedTime / fadeInTime);
            startColor.a = alpha;
            rawImage.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(Graphic graphic, float fadeOutTime)
    {
        float elapsedTime = 0f;
        Color startColor = graphic.color;
        float targetAlpha = 0f; // Set the target alpha

        while (startColor.a > targetAlpha)
        {
            float alpha = Mathf.Lerp(1f, targetAlpha, elapsedTime / fadeOutTime);
            startColor.a = alpha;
            graphic.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(RawImage rawImage, float fadeOutTime)
    {
        float elapsedTime = 0f;
        Color startColor = rawImage.color;
        float targetAlpha = 0f; // Set the target alpha

        while (startColor.a > targetAlpha)
        {
            float alpha = Mathf.Lerp(1f, targetAlpha, elapsedTime / fadeOutTime);
            startColor.a = alpha;
            rawImage.color = startColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
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