using UnityEngine;
using System.Collections;
using Valve.VR;

public class SteamInputHandler : VRInputHandler
{
    protected override void SetUpActions()
    {
        RegisterAction(VRInputHandler.CREATE_SHIELD, SteamVR_Actions.default_GrabGrip, SteamVR_Input_Sources.LeftHand);
    }
    void RegisterAction(string action, SteamVR_Action_Boolean b, SteamVR_Input_Sources s)
    {
        buttons.Add(action, new SteamInputButton(b, s));
    }
}

public class SteamInputButton : InputButton
{
    SteamVR_Action_Boolean button;
    SteamVR_Input_Sources source;
    public SteamInputButton(SteamVR_Action_Boolean b, SteamVR_Input_Sources s)
    {
        button = b;
    }
    public override bool IsDown()
    {
        return button.GetStateDown(source);
    }

    public override bool IsHeld()
    {
        return button.GetState(source);
    }

    public override bool IsUp()
    {
        return button.GetStateUp(source);
    }
}
