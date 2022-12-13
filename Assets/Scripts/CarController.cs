using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Inspiration taken from tutorial: https://youtu.be/Z4HA8zJhGEk

    

    Vector3 originalCarPosition;
    Quaternion originalCarRotation;
    Vector3 originalCameraPosition;
    Quaternion originalCameraRotation;
    Vector3 originalTrackingSpacePosition;
    Quaternion originalTrackingSpaceRotation;


    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private float currentBrakeForce;
    private bool isBraking;
    private float currentAcceleration = 0f;

    private bool hasCrashed;
    private bool crashCameraEnabled;

    [SerializeField] public Transform OVRPlayerController;
    [SerializeField] public Transform TrackingSpace;

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
        originalCarPosition = gameObject.transform.position;
        originalCarRotation = gameObject.transform.rotation;
        originalCameraPosition = OVRPlayerController.transform.position;
        originalCameraRotation = OVRPlayerController.transform.rotation;
        ExecuteAfterTime(5);
        hasCrashed = false;
        crashCameraEnabled = false;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        originalTrackingSpacePosition = TrackingSpace.transform.position;
        originalTrackingSpaceRotation = TrackingSpace.transform.rotation;
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
        CheckCrash();
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
            OVRPlayerController.parent = gameObject.transform;

            OVRPlayerController.transform.position = originalCameraPosition;
            OVRPlayerController.transform.rotation = originalCameraRotation;

            TrackingSpace.transform.localPosition = originalTrackingSpacePosition;
            TrackingSpace.transform.localRotation = originalTrackingSpaceRotation;

            transform.position = originalCarPosition;
            transform.rotation = originalCarRotation;

            hasCrashed = false;
        }

        if (OVRInput.Get(OVRInput.Button.Three))
        {
            crashCameraEnabled = !crashCameraEnabled;
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

    void CheckCrash()
    {
        if (!hasCrashed)
        {
            if (gameObject.transform.rotation.eulerAngles.x > 50 && gameObject.transform.rotation.eulerAngles.x < 310 && crashCameraEnabled)
            {
                Debug.Log("X crash");
                hasCrashed = true;
                OVRPlayerController.parent = null;
                Quaternion newRotation = originalCarRotation;
                newRotation.x = 0;

                TrackingSpace.transform.localRotation = newRotation;
            }
            if (gameObject.transform.rotation.eulerAngles.z > 50 && gameObject.transform.rotation.eulerAngles.z < 310 && crashCameraEnabled)
            {
                Debug.Log("Z crash");
                hasCrashed = true;
                OVRPlayerController.parent = null;
                Quaternion newRotation = originalCarRotation;
                newRotation.z = 0;

                TrackingSpace.transform.localRotation = newRotation;
            }
        }
        
    }

}
