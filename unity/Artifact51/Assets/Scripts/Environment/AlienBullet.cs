using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{
    Rigidbody rBody;
    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

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
        rBody.isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }
}
