using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class VRReferences : MonoBehaviour
{
    public static VRReferences Instance;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public static Transform LeftHand { get => Instance.leftHand; set => Instance.leftHand = value; }
    public static Transform RightHand { get => Instance.rightHand; set => Instance.rightHand = value; }
    public static Transform Head { get => Instance.head; set => Instance.head = value; }

    private void Awake()
    {
        Instance = this;
    }

}

