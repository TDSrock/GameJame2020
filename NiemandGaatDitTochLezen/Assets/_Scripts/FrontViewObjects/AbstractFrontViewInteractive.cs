using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;

abstract public class AbstractFrontViewInteractive : MonoBehaviour, IFrontViewInteractable
{
    public StringReference interactionOnHoverText;
    public virtual  string GetInteractionText()
    {
        return interactionOnHoverText.Value;
    }
    public abstract void OnClick();
    public abstract void OnRelease();
    public virtual void SetInteractive(bool state)
    {
        this.enabled = state;
    }
}
