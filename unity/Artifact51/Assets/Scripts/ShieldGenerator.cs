using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour
{
    public ShieldGrid shieldGridPrefab;
    int maxLevel = 3;
    public int shieldsInWorld = 0;
    public float spawnDistance = 0.3f;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        if (shieldsInWorld < maxLevel && VRInputHandler.OnButtonDown(VRInputHandler.CREATE_SHIELD))
        {
            if (text)
            {
                Destroy(text.gameObject);
                text = null;
            }

            ShieldGrid shields = GameObject.Instantiate(shieldGridPrefab);
            Vector3 fwd = -VRReferences.Head.position + VRReferences.LeftHand.position;
            fwd.y = 0;
            if (fwd.magnitude == 0)
                fwd = VRReferences.Head.forward;
            fwd.Normalize();
            shields.transform.position = VRReferences.LeftHand.position + fwd * spawnDistance;
            shields.transform.forward = fwd;
            shields.Init(this);
            shieldsInWorld++;
        }
    }
}
