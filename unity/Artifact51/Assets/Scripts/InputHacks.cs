using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHacks : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (VRInputHandler.OnButtonUp(VRInputHandler.DEBUG_RESTART_SCENE))
        {
            SceneLoader.ReloadScene();
        }
    }
}
