using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOphysics : MonoBehaviour
{
    public Vector3 targetPosition = Vector3.up * 7;

    public float maxTorque = 10;
    public float maxThrust = 10;

    public float spin = 180;

    public PID rotationPID;
    public PID positionPID;

    Rigidbody body;

    float targetSpin = 0;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 thrust = positionPID.Update(targetPosition, transform.position, Time.fixedDeltaTime);
        thrust = Vector3.ClampMagnitude(thrust, maxThrust * Time.fixedDeltaTime);
        body.AddForce(thrust, ForceMode.VelocityChange);

        targetSpin += spin * Time.fixedDeltaTime;
        targetSpin = targetSpin % 360;

        Vector3 rot = (Quaternion.Euler(0, targetSpin, 0) * Quaternion.Inverse(transform.rotation)).eulerAngles;

        Vector3 torque = rotationPID.Update(Vector3.zero, 
            DeltaAngle(rot, new Vector3(0, 0, 0)), 
            Time.fixedDeltaTime);
        //torque = transform.InverseTransformVector(torque);
        //torque.Scale(new Vector3(1, 0, 1));
        //torque = transform.TransformVector(torque);
        torque = Vector3.ClampMagnitude(torque, maxTorque * Time.fixedDeltaTime);
        body.AddTorque(torque, ForceMode.VelocityChange);
    }

    static Vector3 DeltaAngle(Vector3 cur, Vector3 tar)
    {
        return new Vector3(Mathf.DeltaAngle(cur.x, tar.x), Mathf.DeltaAngle(cur.y, tar.y), Mathf.DeltaAngle(cur.z, tar.z));
    }
}
