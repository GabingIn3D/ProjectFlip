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
        KimmieHouse,
        StudioStage56,
        Villas,
    }

    public enum PhotoItem
    {
        Knife,
        DirectorPhotoFrame,
        OfficeKey,
        RingImprint,
        Cube,
        Sphere,
        HadronPointOfInterest1,
        HadronPointOfInterest2,
        HadronPointOfInterest3,
    }

    public string photoName {  get; set; }
    public string fileLocation {  get; set; }
    public Location gameLocation {  get; set; }
    public PhotoItem[] photoItems {  get; set; }
    public DateTime photoTime { get; set; }
}
