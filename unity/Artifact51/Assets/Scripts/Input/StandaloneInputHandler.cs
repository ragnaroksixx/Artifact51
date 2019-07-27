using UnityEngine;
using System.Collections;

public class StandaloneInputHandler : VRInputHandler
{
    protected override void SetUpActions()
    {
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
        throw new System.NotImplementedException();
    }

    public override bool IsUp()
    {
        throw new System.NotImplementedException();
    }
}
