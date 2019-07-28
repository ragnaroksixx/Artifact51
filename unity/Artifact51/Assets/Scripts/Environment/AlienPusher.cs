using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPusher : MonoBehaviour
{
    public float force = 5;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody b = other.attachedRigidbody;
        Vector3 push = other.transform.position - transform.position;
        push = Vector3.Scale(new Vector3(1, 0, 1), push);
        float angle = 180;
        Vector3 finalPush = push;
        for(int i = -1; i < 2; i++)
        {
            Vector3 p = Quaternion.Euler(0, i * 60, 0) * push.normalized;
            if(Vector3.Angle(p, b.velocity.normalized) < angle)
            {
                finalPush = p;
                angle = Vector3.Angle(push, b.velocity.normalized);
            }
            //Debug.DrawLine(transform.position, transform.position + p * 3, Color.red);
        }
        //Debug.DrawLine(transform.position, transform.position + finalPush * 5, Color.green);
        other.attachedRigidbody.AddForce(finalPush.normalized * force * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
