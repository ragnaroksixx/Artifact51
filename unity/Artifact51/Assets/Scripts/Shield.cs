using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public List<GameObject> caughtObject = new List<GameObject>();
    public bool canCatch = true;
    public int capacity = 2;
    ShieldGrid grid;
    public void Init(ShieldGrid g, int c)
    {
        capacity = c;
        grid = g;
    }
    public void DestroyShield()
    {
        foreach (GameObject obj in caughtObject)
        {
            Destroy(obj);
        }
        Destroy(this.gameObject);
    }

    public void OnHit(GameObject source, Vector3 point)
    {
        if (source.tag == "shield")
        {
            if (grid.isAnimatingUp && source.transform.root != transform.root)
            {
                grid.Return();
            }
            return;
        }
        AlienBullet projectile = source.GetComponent<AlienBullet>();
        if (projectile)
        {
            caughtObject.Add(source);
            projectile.Stop(point);
            if (caughtObject.Count >= capacity)
            {
                DestroyShield();
                grid.RemoveShield(this);
            }
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision.gameObject, collision.GetContact(0).point);
    }

    public void DisableCollision()
    {
        GetComponentInChildren<Collider>().enabled = false;
    }
}
