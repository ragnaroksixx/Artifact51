using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour
{
    public ShieldGrid shieldGridPrefab;
    int maxLevel = 3;
    int currentLevel = 0;
    float chargeTrack = 0;
    float chargeRate = 5;
    public int shieldsInWorld = 0;
    public float spawnDistance = 0.3f;
    public TMP_Text text;
    private void Awake()
    {
        currentLevel = maxLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel > 0 && VRInputHandler.OnButtonDown(VRInputHandler.CREATE_SHIELD))
        {
            ShieldGrid shields = GameObject.Instantiate(shieldGridPrefab);
            Vector3 fwd = -VRReferences.Head.position + VRReferences.LeftHand.position;
            fwd.y = 0;
            if (fwd.magnitude == 0)
                fwd = VRReferences.Head.forward;
            fwd.Normalize();
            shields.transform.position = VRReferences.LeftHand.position + fwd * spawnDistance;
            shields.transform.forward = fwd;
            shields.Init(this);
            currentLevel--;
            shieldsInWorld++;
        }

        if (currentLevel < maxLevel - shieldsInWorld)
        {
            chargeTrack += Time.deltaTime;
            if (chargeTrack >= 1)
            {
                chargeTrack = chargeTrack - 1;
                currentLevel++;
            }
        }
        text.text = "Level: " + currentLevel;
        text.text += "\nCharge: " + chargeTrack;
    }

    public void AddShield(int value)
    {
        currentLevel += value;
        if (currentLevel > maxLevel)
            currentLevel = maxLevel;
    }
}
