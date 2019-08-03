﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : Interactable
{
    public Shield attachedShield;

    public float startLifetime = 30f;
    float lifeTime = 30;
    bool dying = true;
    public GameObject explosion;
    TrailRenderer tRender;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = startLifetime;
        explosion = GameObject.Instantiate(explosion, transform.position, transform.rotation);
        explosion.SetActive(false);
        tRender = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        lifeTime = startLifetime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0 && dying)
        {
            // Death();
            gameObject.SetActive(false);
        }
    }

    public void Death()
    {
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void Stop(Vector3 position)
    {
        rBody.velocity = Vector3.zero;
        transform.position = position;
        rBody.constraints = RigidbodyConstraints.FreezeAll;
        lifeTime = startLifetime;
        if (tRender)
            tRender.emitting = false;
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
        lifeTime = startLifetime;
        dying = true;
        transform.SetParent(BulletPool.instance.transform, true);
        if (tRender)
        {
            tRender.transform.localPosition = Vector3.zero;
            tRender.emitting = true;
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
        lifeTime = startLifetime;
        dying = false;
        if (tRender)
            tRender.emitting = false;
    }
}
