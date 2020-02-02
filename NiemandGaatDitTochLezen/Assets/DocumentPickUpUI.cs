using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SjorsGielen.CustomVariables.ReferenceVariables;

public class DocumentPickUpUI : MonoBehaviour
{
    private static DocumentPickUpUI instance;
    public static DocumentPickUpUI Instance { get { return instance; } }

    public StringReference uiHintText;
    public bool interaction = false;

    public void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }
    public Sprite fallback;
    public Image image;
    public GameObject uiElementRoot;

    public void Update()
    {
        if (interaction)
        {
            uiHintText.Value = "Press any key to close document";
            if (Input.anyKeyDown)
            {

                CloseInteraction();
            }
        }
    }

    public void OpenImageInteraction(Document doc)
    {

        uiElementRoot.SetActive(true);
        if (doc != null)
        {
            ImageObject doci = doc as ImageObject;
            image.sprite = doci.image;
        }
        else
        {
            image.sprite = fallback;
        }
        interaction = true;
    }

    public void CloseInteraction()
    {
        interaction = false;
        uiElementRoot.SetActive(false);
    }
}
