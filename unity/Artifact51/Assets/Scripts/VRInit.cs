using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInit : MonoBehaviour
{
    [SerializeField]
    Transform leftHandRoot, rightHandRoot, headRoot;

    private IEnumerator Start()
    {
        yield return null; //wait for scenes to load
        Transform head = GameObject.Find("Head").transform;
        Transform lhand = GameObject.Find("Left Hand").transform;
        Transform rhand = GameObject.Find("Right Hand").transform;
        AttachToRoot(head, headRoot);
        AttachToRoot(lhand, leftHandRoot);
        AttachToRoot(rhand, rightHandRoot);

    }

    private void AttachToRoot(Transform obj, Transform root)
    {
        obj.SetParent(root);
        obj.localPosition = Vector3.zero;
        obj.localRotation = Quaternion.identity;
        obj.localScale = Vector3.one;
    }
}
