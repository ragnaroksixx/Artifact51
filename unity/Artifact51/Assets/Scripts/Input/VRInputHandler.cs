using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VRInputHandler : MonoBehaviour
{
    protected Dictionary<string, InputButton> buttons = new Dictionary<string, InputButton>();
    protected Dictionary<string, InputAxis> axes = new Dictionary<string, InputAxis>();
    private static VRInputHandler Instance = null;

    public const string CREATE_SHIELD = "createShield";
    private void Awake()
    {
        SetUpActions();
        Instance = this;
    }

    protected abstract void SetUpActions();

    static bool HasButton(string value)
    {
        return Instance.buttons.ContainsKey(value);
    }
    static bool HasAxis(string value)
    {
        return Instance.axes.ContainsKey(value);
    }
    public static bool OnButtonUp(string action)
    {
        return HasButton(action) && Instance.buttons[action].IsUp();
    }
    public static bool OnButtonDown(string action)
    {
        return HasButton(action) && Instance.buttons[action].IsDown();
    }
    public static bool OnButtonHeld(string action)
    {
        return HasButton(action) && Instance.buttons[action].IsHeld();
    }
    public static float GetAxis(string action)
    {
        if (!HasAxis(action))
            return 0;

        return Instance.axes[action].GetAxis();
    }

    public static void Vibrate(bool right,AudioClip audio)
    {
        Instance.VibrateController(right,audio);
    }
    public virtual void VibrateController(bool right,AudioClip audio)
    {

    }

}

public abstract class InputButton
{
    public abstract bool IsUp();
    public abstract bool IsDown();
    public abstract bool IsHeld();
}

public abstract class InputAxis
{
    public virtual float GetAxis()
    {
        return 0;
    }
}
