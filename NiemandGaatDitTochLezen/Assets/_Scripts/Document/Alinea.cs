using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAlinea", menuName = "Document/Alinea")]
public class Alinea : ScriptableObject
{
    [SerializeField]
    President president;
    [SerializeField]
    Country country;

    [TextArea(3, 10)]
    string text;

    public void BuildString()
    {
        text = string.Format(text, president.getPresidentName, country.getCountryName);
    }       
}
