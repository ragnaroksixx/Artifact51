using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    protected Rigidbody rBody;
    public virtual bool IsInteractable()
    {
        return true;
    }
    protected virtual void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }
    public virtual void Grab(Transform hand)
    {
        transform.parent = hand;
        rBody.velocity = Vector3.zero;
        rBody.constraints = RigidbodyConstraints.FreezeAll;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void Release(Vector3 velocity)
    {
        transform.parent = null;
        rBody.constraints = RigidbodyConstraints.None;
    }
}
