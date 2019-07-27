using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienArea : MonoBehaviour
{
    public static AlienArea instance;

    public float outerRadius = 10; 
    public float innerRadius = 5;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //returns a target spot near the alien's position, randomized within an arc.
    public Vector3 GetTarget(Vector3 pos, float arc)
    {
        Vector3 near = transform.position - pos;
        near.Normalize();
        float r = Mathf.Atan2(near.z, -near.x) * Mathf.Rad2Deg;
        r += Random.Range(0, arc) - arc / 2f;
        float dist = Random.Range(innerRadius, outerRadius);
        return Quaternion.Euler(0, r, 0) * Vector3.right * dist;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < 19; i++)
        {
            float r = i / 20f * 360;
            float r2 = (i + 1) / 20f * 360;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position + Quaternion.Euler(0, r, 0) * Vector3.right * innerRadius,
                transform.position + Quaternion.Euler(0, r2, 0) * Vector3.right * innerRadius);
            Gizmos.DrawLine(transform.position + Quaternion.Euler(0, r, 0) * Vector3.right * outerRadius,
                transform.position + Quaternion.Euler(0, r2, 0) * Vector3.right * outerRadius);
        }
    }
}
