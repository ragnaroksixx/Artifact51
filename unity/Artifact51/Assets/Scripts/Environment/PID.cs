using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PID :MonoBehaviour
{
    public float pFactor, iFactor, dFactor;
    public float iClamp;

    Vector3 integral;
    Vector3 lastError;


    public PID(float pFactor, float iFactor, float dFactor)
    {
        this.pFactor = pFactor;
        this.iFactor = iFactor;
        this.dFactor = dFactor;
    }


    public Vector3 Update(Vector3 setpoint, Vector3 actual, float timeFrame)
    {
        Vector3 present = setpoint - actual;
        integral += present * timeFrame;
        integral = new Vector3(
            Mathf.Clamp(integral.x, -iClamp, iClamp), 
            Mathf.Clamp(integral.y, -iClamp, iClamp), 
            Mathf.Clamp(integral.z, -iClamp, iClamp));
        Vector3 deriv = (present - lastError) / timeFrame;
        lastError = present;
        return present * pFactor + integral * iFactor + deriv * dFactor;
    }
}
