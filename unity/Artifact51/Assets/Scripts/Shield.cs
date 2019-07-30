using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public List<GameObject> caughtObject = new List<GameObject>();
    public bool canCatch = true;
    int capacity = 3;
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
            AlienBullet a = obj.GetComponent<AlienBullet>();
            if (a != null)
            {
                a.Death();
            }
            else
            {
                Destroy(obj);
            }
        }
        Destroy(this.gameObject);
    }

    public void OnHit(GameObject source, Vector3 point)
    {
        if (source.tag == "shield")
        {
            if (grid.isAnimatingUp && source.transform.root != transform.root)
            {
                grid.Return(this);
            }
            return;
        }
        AlienBullet projectile = source.GetComponent<AlienBullet>();
        if (projectile)
        {
            if (caughtObject.Contains(source)) return;
            projectile.attachedShield = this;
            caughtObject.Add(source);
            projectile.Stop(point);
            if (caughtObject.Count > capacity)
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
    private void OnCollisionExit(Collision collision)
    {
        Detach(collision.gameObject);
    }
    public void Detach(GameObject g)
    {
        if (caughtObject.Contains(g))
        {
            caughtObject.Remove(g);
        }
    }
    public void DisableCollision()
    {
        GetComponentInChildren<Collider>().enabled = false;
    }
}
