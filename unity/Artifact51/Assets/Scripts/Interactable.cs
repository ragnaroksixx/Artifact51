using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    protected Rigidbody rBody;
    CoroutineHandler destroyRoutine;
    public virtual bool IsInteractable()
    {
        return true;
    }
    protected virtual void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        destroyRoutine = new CoroutineHandler(this);
    }
    public virtual void Grab(Transform hand)
    {
        transform.parent = hand;
        rBody.velocity = Vector3.zero;
        rBody.constraints = RigidbodyConstraints.FreezeAll;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        destroyRoutine.StopCoroutine();
    }

    public virtual void Release(Vector3 velocity)
    {
        transform.parent = null;
        rBody.constraints = RigidbodyConstraints.None;
        destroyRoutine.StartCoroutine(DestroyIn(10));
    }

    IEnumerator DestroyIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
