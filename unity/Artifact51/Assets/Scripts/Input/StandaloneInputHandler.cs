using UnityEngine;
using System.Collections;

public class StandaloneInputHandler : VRInputHandler
{
    protected override void SetUpActions()
    {
        RegisterAction(VRInputHandler.CREATE_SHIELD, KeyCode.Mouse0);
        RegisterAction(VRInputHandler.DEBUG_RESTART_SCENE, KeyCode.R);
    }

    void RegisterAction(string action, KeyCode k)
    {
        buttons.Add(action, new StandAloneInputButton(k));
    }
}

public class StandAloneInputButton : InputButton
{
    KeyCode key;
    public StandAloneInputButton(KeyCode k)
    {
        key = k;
    }
    public override bool IsDown()
    {
        return Input.GetKeyDown(key);
    }

    public override bool IsHeld()
    {
        return Input.GetKey(key);
    }

    public override bool IsUp()
    {
        return Input.GetKeyUp(key);
    }
}
