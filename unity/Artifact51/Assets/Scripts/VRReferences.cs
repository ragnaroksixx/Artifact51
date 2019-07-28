using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class VRReferences : MonoBehaviour
{
    public static VRReferences Instance;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    //Rigidbody leftRigidBody, rightRigidBody;

    public static Transform LeftHand { get => Instance.leftHand; set => Instance.leftHand = value; }
    public static Transform RightHand { get => Instance.rightHand; set => Instance.rightHand = value; }
    public static Transform Head { get => Instance.head; set => Instance.head = value; }
    //public static Rigidbody LeftRigidBody { get => Instance.leftRigidBody; }
    //public static Rigidbody RightRigidBody { get => Instance.rightRigidBody; }

    private void Awake()
    {
        Instance = this;
       //leftRigidBody = leftHand.GetComponent<Rigidbody>();
        //rightRigidBody = rightHand.GetComponent<Rigidbody>();
    }

}

