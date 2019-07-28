using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    List<GameObject> pool;
    const int poolSize = 100;

    public static BulletPool instance;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        pool = new List<GameObject>();
    }

    public static GameObject SpawnBullet()
    {
        return instance.spawnBullet();
    }

    private GameObject spawnBullet()
    {
        if(pool.Count < poolSize)
        {
            GameObject g = GameObject.Instantiate(bulletPrefab, this.transform);
            pool.Add(g);
            return g;
        } else
        {
            foreach(GameObject pooled in pool)
            {
                if(!pooled.activeSelf)
                {
                    pooled.SetActive(true);
                    return pooled;
                }
            }
            return pool[Random.Range(0, pool.Count - 1)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
