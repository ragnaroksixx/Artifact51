using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : Interactable
{
    public Shield attachedShield;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this, 30);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(this.gameObject);
    }

    public void Stop(Vector3 position)
    {
        rBody.velocity = Vector3.zero;
        transform.position = position;
        rBody.constraints = RigidbodyConstraints.FreezeAll;
        //GetComponent<Collider>().enabled = false;
    }

    public override bool IsInteractable()
    {
        return rBody.constraints == RigidbodyConstraints.FreezeAll; ;
    }
    public override void Release(Vector3 velocity)
    {
        GetComponent<Collider>().enabled = true;
        base.Release(velocity);
        rBody.constraints = RigidbodyConstraints.None;
        if (velocity.magnitude >= 0.015f)
            rBody.velocity = velocity.normalized * 30;
        else
        {
            rBody.velocity = velocity * 60;
            rBody.useGravity = true;
        }
    }
    public override void Grab(Transform hand)
    {
        if (attachedShield)
            attachedShield.Detach(gameObject);
        base.Grab(hand);
        gameObject.tag = "bullet";
        GetComponent<Collider>().enabled = false;
        rBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
