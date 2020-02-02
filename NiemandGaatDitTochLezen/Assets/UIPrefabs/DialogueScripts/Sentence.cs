using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence 
{
    public CharacterName character;

    [TextArea(3, 10)]
    public string line;
}
