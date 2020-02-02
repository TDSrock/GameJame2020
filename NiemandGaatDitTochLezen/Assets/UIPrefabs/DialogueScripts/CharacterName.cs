using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCharacters", menuName = "Dialogue/CharacterName")]
public class CharacterName : ScriptableObject
{
    public string name;
    public Sprite characterPortrait;

}
