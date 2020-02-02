using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLetter", menuName ="Document/Letter")]
public class Document : ScriptableObject
{
    public DocumentType docType;
    public enum DocumentType
    {
        Image,
        Letter,
        Ad
    }
}
