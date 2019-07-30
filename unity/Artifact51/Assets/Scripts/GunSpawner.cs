using UnityEngine;
using System.Collections;
using DG.Tweening;
public class GunSpawner : MonoBehaviour
{
    public Gun[] gunPrefabs;
    public Transform rotator;
    float countdownTrack;
    public Gun setGun;
    float rotSpeed = 250;
    float start = 1;
    float delta = 0.25f;
    // Use this for initialization
    void Start()
    {
        Sequence t = DOTween.Sequence()
            .Append(rotator.DOLocalMoveY(start - delta, 1))
            .Append(rotator.DOLocalMoveY(start, 1))
            .SetLoops(-1);
        SpawnGun(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = rotator.transform.localEulerAngles;
        rot.y += Time.deltaTime * rotSpeed;
        if (rot.y > 360)
            rot.y -= 360;
        rotator.transform.localEulerAngles = rot;

        if (setGun != null) return;

        countdownTrack -= Time.deltaTime;
        if (countdownTrack <= 0)
        {
            SpawnGun(Random.Range(0, gunPrefabs.Length));


        }

    }
    void SpawnGun(int index)
    {
        setGun = Instantiate(gunPrefabs[index]);
        setGun.transform.SetParent(rotator);
        setGun.transform.localPosition = Vector3.zero;
        setGun.Init(this);
    }
    public void TakeWeapon()
    {
        setGun = null;
        countdownTrack = 5;
    }

}
