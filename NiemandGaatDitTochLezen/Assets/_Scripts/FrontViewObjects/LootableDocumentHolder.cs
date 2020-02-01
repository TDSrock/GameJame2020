using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;

public class LootableDocumentHolder : AbstractFrontViewInteractive
{
    public Document document;
    bool isClicked = false;
    [SerializeField]
    FloatReference UIMaxTimeSearch;
    [SerializeField]
    FloatReference timeToSearch;
    [SerializeField]
    FloatReference timeSeaching;
    public void Update()
    {
        if(isClicked)
            timeSeaching.Value += Time.deltaTime; 
    }

    public override void OnClick()
    {
        //start searching
        UIMaxTimeSearch.Value = timeToSearch.Value;
        isClicked = true;
    }

    public override void OnRelease()
    {
        //if search complete, pop up item, otherwise reset time searching
        if(timeSeaching >= timeToSearch)
        {
            Debug.Log("OPEN DOCUMENT CODE HERE");
        }

        timeSeaching.Value = 0f;

        isClicked = false;
    }
}
