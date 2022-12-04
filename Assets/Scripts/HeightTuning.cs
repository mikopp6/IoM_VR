using UnityEngine;

public class HeightTuning: MonoBehaviour {

    private Vector2 verticalInput;
    private OVRCameraRig ovrCameraRig;
    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        GetInput();
        AdjustHeight();
    }

    private void GetInput()
    {
        //OVRCameraRig.UpdatedAnchors();
        OVRInput.FixedUpdate();

        if(OVRInput.GetUp(OVRInput.Touch.PrimaryThumbstick))
        {
            verticalInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        }
        else if(OVRInput.GetUp(OVRInput.Touch.SecondaryThumbstick))
        {
            verticalInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        }
        if(OVRInput.GetDown(OVRInput.Touch.PrimaryThumbstick))
        {
            verticalInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) * -1;
        }
        else if(OVRInput.GetDown(OVRInput.Touch.SecondaryThumbstick))
        {
            verticalInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick) * -1;
        }
        else
        {
            verticalInput = Vector2.zero;
        }
    }

    private void AdjustHeight()
    {
        ovrCameraRig.transform.position = verticalInput;
    }
}