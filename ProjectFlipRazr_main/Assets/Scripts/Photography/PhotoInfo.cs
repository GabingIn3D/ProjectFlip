using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhotoInfo
{
    public enum Location
    {
        Unknown,
        House,
        Warehouse,
    }

    public enum PhotoItem
    {
        Knife,
        FamilyPortrait,
    }

    public string photoName {  get; set; }
    public string fileLocation {  get; set; }
    public Location gameLocation {  get; set; }
    public PhotoItem[] photoItems {  get; set; }
    public DateTime photoTime { get; set; }
}
