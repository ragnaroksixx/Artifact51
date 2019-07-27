using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAI : MonoBehaviour
{
    public Transform moveTarget;
    public Transform aimTarget;
    public float moveSpeed = 10;
    public float moveAccel = 10;
    public float stopRadius = 1;

    public GameObject cannon;
    public GameObject bullet;
    public float fireRate = 1;
    public float bulletSpeed = 30;
    Rigidbody body;
    float bulletTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = aimTarget.position - transform.position;
        transform.rotation = Quaternion.Euler(0, -90 -Mathf.Atan2(-target.z, -target.x) * Mathf.Rad2Deg, 0);
        cannon.transform.rotation = Quaternion.LookRotation((aimTarget.position - cannon.transform.position).normalized, Vector3.up);
    }

    private void FixedUpdate()
    {
        Vector3 targetVel = Vector3.zero;
        if(moveTarget != null)
        {
            targetVel = moveTarget.position - transform.position;
        } else
        {
            targetVel = Vector3.zero - transform.position; //move to the origin if target is null6
        }
        targetVel = new Vector3(targetVel.x, 0, targetVel.z);

        if (targetVel.magnitude < stopRadius)
        {
            targetVel = Vector3.zero; //come to a stop.
        } else
        {
            targetVel = targetVel.normalized * moveSpeed; //move at move speed.
        }

        Vector3 currentVel = Vector3.Scale(new Vector3(1, 0, 1), body.velocity);
        Vector3 diff = targetVel - currentVel;
        if (diff.magnitude > moveAccel * Time.fixedDeltaTime)
        {
            diff = diff.normalized * moveAccel * Time.fixedDeltaTime;
        }
        body.AddForce(diff, ForceMode.VelocityChange);
        bulletTimer += Time.fixedDeltaTime;
        while(fireRate > 0 && bulletTimer > 1/fireRate)
        {
            GameObject b = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);
            Rigidbody bBody = b.GetComponent<Rigidbody>();
            bBody.velocity = cannon.transform.forward * bulletSpeed;
            bulletTimer -= 1/fireRate;
        }
    }
}
