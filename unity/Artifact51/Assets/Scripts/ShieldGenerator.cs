using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour
{
    public ShieldGrid shieldGridPrefab;

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
            ShieldGrid shields = GameObject.Instantiate(shieldGridPrefab);
            shields.transform.position = VRReferences .LeftHand.position;
            shields.Init();
        }
    }
}
