using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;
using UnityEngine.UI;

public class FillSetter : MonoBehaviour
{
    [SerializeField]
    Image backdrop;
    [SerializeField]
    Image fillElement;
    [SerializeField]
    Text textElement;
    [SerializeField]
    FloatReference current;
    [SerializeField]
    FloatReference max;

    private void Awake()
    {
        current.Value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(current.Value == 0)
        {
            //disable the visual components
            textElement.enabled = false;
            backdrop.color = new Color(backdrop.color.r, backdrop.color.g, backdrop.color.b, 0);
            fillElement.color = new Color(fillElement.color.r, fillElement.color.g, fillElement.color.b, 0);
        }
        else
        {
            //enable the visual components
            textElement.enabled = true;
            backdrop.color = new Color(backdrop.color.r, backdrop.color.g, backdrop.color.b, 1);
            fillElement.color = new Color(fillElement.color.r, fillElement.color.g, fillElement.color.b, 1);
            fillElement.fillAmount =Mathf.Clamp( current.Value / max.Value, 0, 1);
        }
    }
}
