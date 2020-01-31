using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPresident", menuName = "Document/President")]
public class President : ScriptableObject
{
    public string getPresidentName { get { return presidentName; } private set { presidentName = value; } }
    public Sprite getPresidentPortrait { get { return presidentPortrait; } private set { presidentPortrait = value; } }

    [SerializeField]
    string presidentName;
    [SerializeField]
    Sprite presidentPortrait;
}
