using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAlinea", menuName = "Document/Image")]
public class ImageObject : Document
{
    [SerializeField]
    Sprite image;
}
