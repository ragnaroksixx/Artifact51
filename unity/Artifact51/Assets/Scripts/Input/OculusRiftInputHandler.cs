using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusRiftInputHandler : VRInputHandler
{
    CoroutineHandler vibrationCoroutine;
    private void Start()
    {
        vibrationCoroutine = new CoroutineHandler(this);
    }
    protected override void SetUpActions()
    {
        buttons.Clear();
        axes.Clear();
        RegisterAction(VRInputHandler.CREATE_SHIELD, OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        RegisterAction(VRInputHandler.DEBUG_RESTART_SCENE, OVRInput.Button.One, OVRInput.Controller.LTouch);
       // RegisterAction(VRInputHandler.UI_SELECTION_LEFT, OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
    }
    void RegisterAction(string key, OVRInput.Button b, OVRInput.Controller c)
    {
        buttons.Add(key, new RiftInputButton(b, c));
    }
    void RegisterAction(string key, OVRInput.Axis1D a, OVRInput.Controller c)
    {
        axes.Add(key, new RiftInputAxis(a, c));
    }
    public override void VibrateController(bool right, AudioClip audio)
    {
        base.VibrateController(right, audio);
        if (OVRPlugin.GetSystemHeadsetType() == OVRPlugin.SystemHeadset.None)
            return;
        StartCoroutine(VibrationCoroutine(right ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch, audio.length));
    }
    IEnumerator VibrationCoroutine(OVRInput.Controller c, float duration)
    {
        OVRInput.SetControllerVibration(0.4f, 0.4f, c);
        yield return new WaitForSeconds(0.25f);
        OVRInput.SetControllerVibration(0, 0, c);
    }
}

public class RiftInputButton : InputButton
{
    OVRInput.Button button;
    OVRInput.Controller controller;
    public RiftInputButton(OVRInput.Button b, OVRInput.Controller c)
    {
        button = b;
        controller = c;
    }
    public override bool IsDown()
    {
        return OVRInput.GetDown(button, controller);
    }

    public override bool IsHeld()
    {
        return OVRInput.Get(button, controller);
    }

    public override bool IsUp()
    {
        return OVRInput.GetUp(button, controller);
    }
}
public class RiftInputAxis : InputAxis
{
    OVRInput.Axis1D axis;
    OVRInput.Controller controller;
    public RiftInputAxis(OVRInput.Axis1D a, OVRInput.Controller c)
    {
        axis = a;
        controller = c;
    }
    public override float GetAxis()
    {
        return OVRInput.Get(axis, controller);
    }
}
