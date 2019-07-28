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

        yield return null; //allow headset to set initial position
        yield return null; //allow headset to set initial position
        Vector3 newPos = -headRoot.transform.localPosition;
        newPos.y = headRoot.transform.root.position.y;
        //headRoot.transform.root.position = newPos;
    }

    private void AttachToRoot(Transform obj, Transform root)
    {
        obj.SetParent(root);
        obj.localPosition = Vector3.zero;
        obj.localRotation = Quaternion.identity;
        obj.localScale = Vector3.one;
    }
}
