using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PostedImage : ScriptableObject
{
    public List<string> Comments = new List<string>();

    public GameObject Parent;
}
