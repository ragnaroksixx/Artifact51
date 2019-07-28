using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienUFO : MonoBehaviour
{
    UFOphysics ufoPhys;

    public Vector3 dropStart = Vector3.right * 5;
    public Vector3 dropEnd = Vector3.forward * 5;
    public float dropDuration = 10;
    public List<GameObject> dropAliens;
    public LayerMask raycastMask;

    const float DESCENTRADIUS = 1;
    const float DESTROYALTITUDE = 50;
    const float RAYCASTLENGTH = 10;
    bool descending = true;

    int dropTotal = 0;
    float dropTimer = 0;
    float dropDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        ufoPhys = GetComponent<UFOphysics>();
        dropTotal = dropAliens.Count;
        ufoPhys.targetPosition = dropStart;
        dropDelay = Random.Range(0, (dropTimer / dropTotal));
    }
    //public void S
    // Update is called once per frame
    void Update()
    {
        if((transform.position - dropStart).magnitude < DESCENTRADIUS)
        {
            descending = false;
        }

        if(!descending)
        {
            dropTimer += Time.deltaTime;
            
            if(dropTimer >= (dropTotal - dropAliens.Count) * (dropDuration / dropTotal) + dropDelay && dropAliens.Count > 0)
            {
                Vector3 target = transform.position + Vector3.down * RAYCASTLENGTH;
                Ray ray = new Ray(transform.position, Vector3.down);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, RAYCASTLENGTH, raycastMask))
                {
                    target = hit.point;
                }
                GameObject.Instantiate(dropAliens[0], target, Quaternion.identity);
                dropAliens.RemoveAt(0);
                dropDelay = Random.Range(0, (dropTimer / dropTotal));
            }

            if (dropTimer > dropDuration)
            {
                ufoPhys.targetPosition = dropEnd + Vector3.up * DESTROYALTITUDE * 2;
                if(transform.position.y > DESTROYALTITUDE)
                {
                    Destroy(this.gameObject);
                }
            } else
            {
                ufoPhys.targetPosition = Vector3.Lerp(dropStart, dropEnd, dropTimer / dropDuration);
            }
        }
    }
}
