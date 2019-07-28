using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;

public class Gun : Interactable
{
    public GameObject bullet;
    public float bulletSpeed = 30;
    public Transform nozzle;
    Tween recoilTween;
    Tween punchTween;
    Vector3 startPos;
    public TMP_Text text;
    public int ammo;
    GunSpawner spawner;
    PlayerGunHandler gunHandler;
    public float gunCooldown;
    protected override void Awake()
    {
        base.Awake();
        startPos = transform.localPosition;
        text.text = ammo.ToString(); ;
    }
    public void Init(GunSpawner gs)
    {
        spawner = gs;
    }
    public void Shoot()
    {
        if (ammo <= 0) return;

        GameObject b = GameObject.Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation);
        Rigidbody bBody = b.GetComponent<Rigidbody>();
        bBody.velocity = nozzle.transform.forward * bulletSpeed;
        Recoil();
        ammo--;
        text.text = ammo.ToString(); ;
    }

    public void Recoil()
    {
        if (recoilTween != null)
            recoilTween.Kill(true);
        if (punchTween != null)
            punchTween.Kill(true);
        Transform toRecoil = VRReferences.RightHand.GetChild(0);
        float duration = .2f;
        recoilTween = toRecoil.DOLocalRotate(new Vector3(-25, 0, 0), duration * .6f);
        recoilTween.OnComplete(() =>
        {
            recoilTween = toRecoil.DOLocalRotate(new Vector3(0, 0, 0), duration * .4f);
        }
        );
        punchTween = toRecoil.DOLocalMoveZ(-0.15F, duration * .5f);
        punchTween.OnComplete(() =>
        {
            punchTween = toRecoil.DOLocalMoveZ(startPos.z, duration * .4f);
        }
);
    }
    public override void Release(Vector3 velocity)
    {
        base.Release(velocity);
        rBody.constraints = RigidbodyConstraints.None;

        rBody.velocity = velocity * 125;
        rBody.useGravity = true;
        if(gunHandler)
        {
            gunHandler.gun = null;
            gunHandler = null;
        }
    }

    public override void Grab(Transform hand)
    {
        base.Grab(hand);
        if (spawner)
        {
            spawner.TakeWeapon();
            spawner = null;
        }
    }

}
