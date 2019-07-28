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
    public float fireRate = 1;
    public float fireRandomness = 1.5f;
    public float bulletSpeed = 30;
    Rigidbody body;
    float bulletTimer = 0;
    Collider playerHead;
    public Animator legsAnim, torsoAnim;
    public Transform torso;
    bool stopped = false;
    int shootsBeforeMove = 3;
    int shootsTrack;
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
        if (LevelManager.Instance.isGameOver)
        {
            legsAnim.SetBool("isMoving", false);
            torsoAnim.SetBool("isMoving", false);
            return;
        }
        Vector3 target = playerHead.transform.position - transform.position;

        torso.rotation = Quaternion.LookRotation((playerHead.transform.position - cannon.transform.position).normalized, Vector3.up);
        cannon.transform.rotation = Quaternion.LookRotation((playerHead.transform.position - cannon.transform.position).normalized, Vector3.up);
        legsAnim.SetBool("isMoving", !stopped);
        torsoAnim.SetBool("isMoving", !stopped);
    }

    private void FixedUpdate()
    {
        if (LevelManager.Instance.isGameOver)
        {
            return;
        }
        Vector3 targetVel = Vector3.zero;
        if (moveTarget != null)
        {
            targetVel = moveTarget - transform.position;
            transform.rotation = Quaternion.Euler(0, -90 - Mathf.Atan2(-targetVel.z, -targetVel.x) * Mathf.Rad2Deg, 0);
        }
        else
        {
            targetVel = Vector3.zero - transform.position; //move to the origin if target is null
            Vector3 target = playerHead.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0, -90 - Mathf.Atan2(-target.z, -target.x) * Mathf.Rad2Deg, 0);
        }
        targetVel = new Vector3(targetVel.x, 0, targetVel.z);

        if (targetVel.magnitude < stopRadius)
        {
            targetVel = Vector3.zero; //come to a stop.
            stopped = true;
        }
        else
        {
            targetVel = targetVel.normalized * moveSpeed; //move at move speed.
            stopped = false;
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
                GameObject b = BulletPool.SpawnBullet();
                b.transform.position = cannon.transform.position;
                Rigidbody bBody = b.GetComponent<Rigidbody>();
                bBody.velocity = cannon.transform.forward * bulletSpeed;
                bulletTimer -= 1 / fireRate * Random.Range(1, fireRandomness);
                legsAnim.SetTrigger("shoot");
                torsoAnim.SetTrigger("shoot");
                shootsTrack++;
            }
            if (shootsTrack > shootsBeforeMove)
            {
                shootsTrack = 0;
                moveTarget = AlienArea.instance.GetTarget(transform.position, moveTargetArc);

            }
        }
    }
}
