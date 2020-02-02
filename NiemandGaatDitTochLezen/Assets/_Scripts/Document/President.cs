using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPresident", menuName = "Document/President")]
public class President : ScriptableObject
{
    public string getPresidentName { get { return presidentName; } private set { presidentName = value; } }
    public Sprite getPresidentPortrait { get { return presidentPortrait; } private set { presidentPortrait = value; } }
    public Country getHomeCOuntry { get { return homeCountry; } private set { homeCountry = value; } }

    public List<ImageObject> whenCorruptNotes = new List<ImageObject>();
    public List<ImageObject> whenCleanNotes = new List<ImageObject>();

    public bool isClean = false;

    [SerializeField]
    string presidentName;
    [SerializeField]
    Country homeCountry;
    [SerializeField]
    Sprite presidentPortrait;
}
