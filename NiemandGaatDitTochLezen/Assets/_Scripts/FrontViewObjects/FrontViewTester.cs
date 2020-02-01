using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontViewTester : FrontViewSuper
{
    public bool isInteractingWith = false;
    public KeyCode closeInteractionKey = KeyCode.P;

    public void Update()
    {
        if (isInteractingWith)
        {
            if (Input.GetKeyUp(closeInteractionKey))
            {
                this.isInteractingWith = false;
                this.CloseInteraction();
            }
        }
    }

    public override void SetupInteraction()
    {
        isInteractingWith = true;
    }
}
