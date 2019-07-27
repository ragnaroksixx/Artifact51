using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGrid : MonoBehaviour
{
    public List<Shield> shields;
    public Shield centerShield;
    ShieldGenerator source;
    public float duration = 5;
    public void Init(ShieldGenerator s)
    {
        source = s;
        foreach (Shield shield in shields)
        {
            shield.Init(this, 2);
        }
        centerShield.Init(this, 9999);
    }
    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            foreach (Shield s in shields)
                s.DestroyShield();
            if (centerShield)
                centerShield.DestroyShield();
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        source.shieldsInWorld--;
    }
    public void RemoveShield(Shield s)
    {
        if (shields.Contains(s))
            shields.Remove(s);
    }
}
