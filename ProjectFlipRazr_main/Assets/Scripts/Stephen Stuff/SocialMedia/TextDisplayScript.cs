using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TextDisplayScript : MonoBehaviour
{
    public string textValue;
    public TextMeshProUGUI textElement;

    public List<string> genericComments;

    // Start is called before the first frame update
    void Start()
    {
        //textElement = GetComponent<TextMeshProUGUI>();
        textElement.text = textValue;
        textElement.enabled = false;
    }

    public void DisplayMessage()
    {
        textElement.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            DisplayMessage();
        }
    }
}
