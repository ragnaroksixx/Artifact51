using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float pitchMax = 2;
    public LayerMask explosionMask;
    public float explosionForce = 10;
    public float explosionRadius = 10;
    public int explosionRays = 30;

    private void OnEnable()
    {
        AudioSource sound = GetComponent<AudioSource>();
        sound.pitch = Random.Range(1, pitchMax);
        sound.Play();

        foreach (ParticleSystem p in GetComponents<ParticleSystem>())
        {
            p.Play();
        }

        for (int i = 0; i < explosionRays; i++)
        {
            Ray ray = new Ray(transform.position, Random.onUnitSphere);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, explosionRadius, explosionMask))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForceAtPosition(ray.direction * explosionForce, hit.point, ForceMode.Impulse);
                }
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
