using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCountry", menuName = "Document/Country")]
public class Country : ScriptableObject
{
    public string getCountryName { get { return countryName; } private set { countryName = value; } }

    [SerializeField]
    string countryName;
}
