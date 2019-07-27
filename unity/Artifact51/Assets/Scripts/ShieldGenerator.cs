using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour
{
    public ShieldGrid sheieldGridPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (VRInputHandler.OnButtonDown(VRInputHandler.CREATE_SHIELD))
        {
            Debug.LogError("RARW");
        }
    }
}
