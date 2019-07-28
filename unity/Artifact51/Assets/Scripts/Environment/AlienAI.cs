using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAI : MonoBehaviour
{
    Vector3 moveTarget = Vector3.one * 10;
    public float moveSpeed = 10;
    public float moveAccel = 10;
    public float stopRadius = 1;
    public float moveTargetArc = 135;

    public GameObject cannon;
    public GameObject bullet;
    public float fireRate = 1;
    public float fireRandomness = 1.5f;
    public float bulletSpeed = 30;
    Rigidbody body;
    float bulletTimer = 0;
    Collider playerHead;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        if (VRReferences.Instance == null)
        {
            GameObject tHead = new GameObject("No vr instance");
            playerHead = tHead.AddComponent<SphereCollider>();
            tHead.transform.position = Vector3.zero;
        }
        else
        {
            playerHead = VRReferences.Head.GetComponentInChildren<Collider>();
        }

        moveTarget = AlienArea.instance.GetTarget(transform.position, moveTargetArc);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = playerHead.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, -90 -Mathf.Atan2(-target.z, -target.x) * Mathf.Rad2Deg, 0);
        cannon.transform.rotation = Quaternion.LookRotation((playerHead.transform.position - cannon.transform.position).normalized, Vector3.up);
    }

    private void FixedUpdate()
    {
        bool stopped = false;
        Vector3 targetVel = Vector3.zero;
        if(moveTarget != null)
        {
            targetVel = moveTarget - transform.position;
        } else
        {
            targetVel = Vector3.zero - transform.position; //move to the origin if target is null
        }
        targetVel = new Vector3(targetVel.x, 0, targetVel.z);

        if (targetVel.magnitude < stopRadius)
        {
            targetVel = Vector3.zero; //come to a stop.
            stopped = true;
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
        if (stopped)
        {
            bulletTimer += Time.fixedDeltaTime;
            while (fireRate > 0 && bulletTimer > 1 / fireRate)
            {
                GameObject b = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation);
                Rigidbody bBody = b.GetComponent<Rigidbody>();
                bBody.velocity = cannon.transform.forward * bulletSpeed;
                bulletTimer -= 1 / fireRate * Random.Range(1, fireRandomness);
            }
        }
    }
}
