using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider FrontCollider;
    [SerializeField] WheelCollider RearCollider;

    [SerializeField] Transform FrontTransform;
    [SerializeField] Transform RearTransform;

    public float acceleration = 500f;
    public float brakingForce = 500f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    private float currentTurnAngle = 0f;
    private float turn = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        OVRInput.FixedUpdate();
        currentAcceleration = acceleration * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
            currentBrakeForce = brakingForce;
        else
            currentBrakeForce = 0f;

        FrontCollider.motorTorque = currentAcceleration;
        RearCollider.motorTorque = currentAcceleration;

        FrontCollider.brakeTorque = currentBrakeForce;
        RearCollider.brakeTorque = currentBrakeForce;


        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            turn = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) * -1;
        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            turn = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        else
            turn = 0f;

        currentTurnAngle = maxTurnAngle * turn;
        FrontCollider.steerAngle = currentTurnAngle;

        UpdateWheel(FrontCollider, FrontTransform);
        UpdateWheel(RearCollider, RearTransform);
    }

    void UpdateWheel(WheelCollider col, Transform trans) 
    {
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }
}
