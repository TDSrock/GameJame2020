using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SjorsGielen.CustomVariables.ReferenceVariables;

public class TextSetter : MonoBehaviour
{
    public StringReference textReference;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = textReference.Value;
    }
}
