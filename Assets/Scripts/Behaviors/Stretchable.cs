using System;
using UnityEngine;

public class Stretchable : TwoHandGrabbable
{
    private Vector3 InitialScale;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        InitialScale = gameObject.transform.localScale;
        base.GrabBegin(hand, grabPoint);
    }

    public override void OnStretch(float amount)
    {
        Debug.Log($"Stretching {gameObject.name}");
        gameObject.transform.localScale = InitialScale * amount;
    }
}
