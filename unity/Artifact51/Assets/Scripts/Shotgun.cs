using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;

class Shotgun : Gun
{
    public Transform[] nozzles;
    public override void Shoot()
    {
        if (ammo <= 0) return;
        base.Shoot();
        foreach (Transform item in nozzles)
        {
            ShootFrom(item);
        }
    }
}

