using UnityEngine;
using System.Collections;

public class PlayerGunHandler : MonoBehaviour
{
    public Gun gun;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gun && VRInputHandler.OnButtonUp(VRInputHandler.SHOOT))
        {
            gun.Shoot();
        }
    }


}
