using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // https://youtu.be/Z4HA8zJhGEk

    Vector3 originalPosition;
    Quaternion originalRotation;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private float currentBrakeForce;
    private bool isBraking;
    private float currentAcceleration = 0f;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider FrontLeftCollider;
    [SerializeField] private WheelCollider FrontRightCollider;
    [SerializeField] private WheelCollider BackLeftCollider;
    [SerializeField] private WheelCollider BackRightCollider;

    [SerializeField] private Transform FrontLeftTransform;
    [SerializeField] private Transform FrontRightTransform;
    [SerializeField] private Transform BackLeftTransform;
    [SerializeField] private Transform BackRightTransform;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void GetInput()
    {
        OVRInput.FixedUpdate();
        currentAcceleration = motorForce * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        isBraking = OVRInput.Get(OVRInput.RawButton.LIndexTrigger);

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            horizontalInput = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) * -1;
        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            horizontalInput = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        else
            horizontalInput = 0f;

        if (OVRInput.Get(OVRInput.Button.One))
        { 
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }

    private void HandleMotor()
    {
        FrontLeftCollider.motorTorque = currentAcceleration;
        FrontRightCollider.motorTorque = currentAcceleration;
        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }
    private void ApplyBraking()
    {
        FrontLeftCollider.brakeTorque = currentBrakeForce;
        FrontRightCollider.brakeTorque = currentBrakeForce;
        BackLeftCollider.brakeTorque = currentBrakeForce;
        BackRightCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        steerAngle = maxSteerAngle * horizontalInput;
        FrontLeftCollider.steerAngle = steerAngle;
        FrontRightCollider.steerAngle = steerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FrontLeftCollider, FrontLeftTransform);
        UpdateSingleWheel(FrontRightCollider, FrontRightTransform);
        UpdateSingleWheel(BackLeftCollider, BackLeftTransform);
        UpdateSingleWheel(BackRightCollider, BackRightTransform);

    }

    void UpdateSingleWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }

}
