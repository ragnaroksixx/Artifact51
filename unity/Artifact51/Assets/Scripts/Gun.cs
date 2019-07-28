using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 30;
    Rigidbody body;
    public Transform nozzle;
    Tween recoilTween;
    Tween punchTween;
    Vector3 startPos;
    private void Awake()
    {
        startPos = transform.localPosition;
    }
    public void Shoot()
    {

        GameObject b = GameObject.Instantiate(bullet, nozzle.transform.position, nozzle.transform.rotation);
        Rigidbody bBody = b.GetComponent<Rigidbody>();
        bBody.velocity = nozzle.transform.forward * bulletSpeed;
        Recoil();
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

}
