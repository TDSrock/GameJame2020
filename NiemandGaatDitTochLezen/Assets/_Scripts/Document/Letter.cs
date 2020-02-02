using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLetter", menuName = "Document/Letter")]
public class Letter : Document
{
    
    [SerializeField]
    President president;
    [SerializeField]
    Country country;

    [SerializeField]
    [TextArea(3, 10)]
    string text;

    private void Awake()
    {
        this.docType = DocumentType.Letter;
    }

    public void BuildString()
    {
        text = string.Format(text, president.getPresidentName, country.getCountryName);
    }       
}
