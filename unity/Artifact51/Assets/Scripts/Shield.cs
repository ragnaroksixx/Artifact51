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

    public void OnHit(GameObject source)
    {
        caughtObject.Add(source);
        if (caughtObject.Count >= capacity)
        {
            DestroyShield();
            grid.RemoveShield(this);
        }
    }
}
