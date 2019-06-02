using System;
using UnityEngine;

public abstract class TwoHandGrabbable : OVRGrabbable {
    private bool IsTwoHandGrabbing = false;
    private float InitialDistance;

    public void Update() {
        if (IsTwoHandGrabbing)
        {
            var ratio = GetControllerDistance() / InitialDistance;
            // Debug.Log($"Setting ratio to {ratio}");
            OnStretch(ratio);
        }
    }

    override public void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if (m_grabbedBy)
        {
            InitialDistance = GetControllerDistance();
            IsTwoHandGrabbing = true;
            Debug.Log("Setting IsTwoHandGrabbing to true");
        }
        base.GrabBegin(hand, grabPoint);
    }

    override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        var flex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_grabbedBy.m_controller);
        Debug.Log($"Flex is {flex}");
        if (flex > m_grabbedBy.grabEnd) {
            Debug.Log("Ignoring GrabEnd because grab button is still held down");
            return;
        }

        IsTwoHandGrabbing = false;
        base.GrabEnd(linearVelocity, angularVelocity);
        Debug.Log("Setting IsTwoHandGrabbing to false");
    }

    public abstract void OnStretch(float amount);

    private float GetControllerDistance()
    {
        var leftPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        var rightPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        return Vector3.Distance(leftPos, rightPos);
    }
}