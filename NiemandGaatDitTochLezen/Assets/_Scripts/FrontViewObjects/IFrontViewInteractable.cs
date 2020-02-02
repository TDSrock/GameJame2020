using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFrontViewInteractable
{
    void OnClick();
    void OnRelease();
    void SetInteractive(bool state);
    string GetInteractionText();
}
