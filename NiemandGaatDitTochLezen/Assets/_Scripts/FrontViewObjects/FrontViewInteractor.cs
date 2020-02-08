using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;

public class FrontViewInteractor : MonoBehaviour
{
    public StringReference interactionHelpText;
    public LayerMask layerMask;
    Camera cam;
    public IFrontViewInteractable mostRecentInteractedInteractable;
    private void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        //turn mouse on
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        //turn mouse off
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        Debug.DrawRay(ray.origin, ray.direction * 30, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, 3000f, layerMask))
        {
            IFrontViewInteractable interactable = hit.collider.gameObject.GetComponent<IFrontViewInteractable>();
            
            if(interactable != null)
            {
                interactionHelpText.Value = interactable.GetInteractionText();
                if (Input.GetMouseButtonDown(0))
                {
                    mostRecentInteractedInteractable = interactable;
                    mostRecentInteractedInteractable.OnClick();
                }
            }
            else
            {
                interactionHelpText.Value = "";
            }
        }
        else
        {
            interactionHelpText.Value = "";
        }

        if (mostRecentInteractedInteractable != null)
        {
            
            if (Input.GetMouseButtonUp(0))
            {
                mostRecentInteractedInteractable.OnRelease();
            }
        }
    }
}
