using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.CustomVariables.ReferenceVariables;

public class PullOutGameIsStrong : AbstractFrontViewInteractive
{
    bool isClicked = false;
    public FloatReference pushAndPullSpeed;
    public FloatRangeReference PulloutRange;
    Vector2 prevMousePos;

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            //track the players pull out or push in input.
            float pushOrPullOutGame = Input.mousePosition.y - prevMousePos.y;
            float isStrong = pushOrPullOutGame * pushAndPullSpeed + this.transform.localPosition.x;//haha lol hardcoded to x axis
            //make is strong weaker. not all can pull out perfectly
            isStrong = Mathf.Clamp(isStrong, PulloutRange.Value.MinValue, PulloutRange.Value.MaxValue);
            this.transform.localPosition = new Vector3(isStrong, transform.localPosition.y, transform.localPosition.z);
        }
        prevMousePos = Input.mousePosition;
    }

    public override void OnClick()
    {
        Debug.LogFormat("{0} was clicked", this.gameObject.name);
        isClicked = true;
    }

    public override void OnRelease()
    {
        Debug.LogFormat("{0} was released", this.gameObject.name);
        isClicked = false;
    }
}
