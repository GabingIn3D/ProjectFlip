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
        KimmiesHouse,
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

    private static readonly Dictionary<Location, string> locationStringMap = new Dictionary<Location, string>
    {
        { Location.Unknown, "Unknown" },
        { Location.KimmiesHouse, "Kimmie's House" },
        { Location.StudioStage56, "Studio Stage 56" },
        { Location.Villas, "Villas" },
    };

    private static readonly Dictionary<PhotoItem, string> photoItemStringMap = new Dictionary<PhotoItem, string>
    {
        { PhotoItem.Knife, "Chef's Knife" },
        { PhotoItem.DirectorPhotoFrame, "Director's Picture" },
        { PhotoItem.OfficeKey, "Key to Office" },
        { PhotoItem.RingImprint, "Ring Finger Tanline" },
        { PhotoItem.Cube, "Cubed" },
        { PhotoItem.Sphere, "Crazoonga" },
        { PhotoItem.HadronPointOfInterest1, "Hadron POI 1" },
        { PhotoItem.HadronPointOfInterest2, "Hadron POI 2" },
        { PhotoItem.HadronPointOfInterest3, "Hadron POI 3" }
    };

    public string photoName {  get; set; }
    public string fileLocation {  get; set; }
    public Location gameLocation {  get; set; }
    public PhotoItem[] photoItems {  get; set; }
    public DateTime photoTime { get; set; }

    public string GetLocationString()
    {
        return locationStringMap.ContainsKey(gameLocation) ? locationStringMap[gameLocation] : "Unknown";
    }

    public string GetPhotoItemString(PhotoItem item)
    {
        return photoItemStringMap.ContainsKey(item) ? photoItemStringMap[item] : "Unknown";
    }
}
