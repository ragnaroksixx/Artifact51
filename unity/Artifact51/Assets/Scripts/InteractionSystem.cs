using UnityEngine;
using System.Collections;

public class InteractionSystem : MonoBehaviour
{
    Interactable leftHandObj, rightHandObj;
    float radius = .1f;
    Vector3 lastPosLeft, lastPosRight;
    public PlayerGunHandler pgh;
    // Use this for initialization
    void Start()
    {
        rightHandObj = pgh.gun;
    }

    // Update is called once per frame
    void Update()
    {
        if (VRInputHandler.OnButtonDown(VRInputHandler.INTERACT_LEFT))
        {
            TryGrab(VRReferences.LeftHand, radius, ref leftHandObj, false);
        }
        else if (VRInputHandler.OnButtonUp(VRInputHandler.INTERACT_LEFT))
        {
            if (leftHandObj)
            {
                leftHandObj.Release(VRReferences.LeftHand.position - lastPosLeft);
                leftHandObj = null;
            }
        }
        if (VRInputHandler.OnButtonDown(VRInputHandler.INTERACT_RIGHT))
        {
            TryGrab(VRReferences.RightHand, radius, ref rightHandObj);
        }
        else if (VRInputHandler.OnButtonUp(VRInputHandler.INTERACT_RIGHT))
        {
            if (rightHandObj)
            {
                rightHandObj.Release(VRReferences.RightHand.position - lastPosRight);
                rightHandObj = null;
            }
        }
        lastPosLeft = VRReferences.LeftHand.position;
        lastPosRight = VRReferences.RightHand.position;
    }

    public void TryGrab(Transform hand, float radius, ref Interactable interactObj, bool canGrabGun = true)
    {
        if (interactObj != null) return;
        Collider[] cols = Physics.OverlapSphere(hand.position, radius);
        Interactable temp;
        float dist;
        float closest = Mathf.Infinity;
        foreach (Collider collider in cols)
        {
            temp = collider.gameObject.GetComponentInParent<Interactable>();
            if (temp == null || !temp.IsInteractable() || (temp is Gun && !canGrabGun)) continue;
            dist = Vector3.Distance(temp.transform.position, hand.position);
            if (dist < closest)
            {
                closest = dist;
                interactObj = temp;
            }
        }
        if (interactObj)
        {
            interactObj.Grab(hand.GetChild(0));
        }
        if (interactObj is Gun)
        {
            pgh.gun = ((Gun)interactObj);
        }
    }
}
