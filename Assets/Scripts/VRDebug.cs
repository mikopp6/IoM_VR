using UnityEngine;

public class VRDebug : MonoBehaviour
{
    // Inspiration taken from tutorial: https://www.youtube.com/watch?v=h0OGk4fwVDg

    public GameObject UI;
    public GameObject UIAnchor;
    private bool UIActive;

    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
        UIActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            UIActive = !UIActive;
            UI.SetActive(UIActive);

            if (UIActive)
            {
                Debug.Log("Have fun!");
                Debug.Log("Press Y to close this window");
                Debug.Log("Press X to enable/disable crash camera");
                Debug.Log("Press A to reset car position");
                Debug.Log("Use the Grip buttons to steer");
                Debug.Log("Right trigger to brake");
                Debug.Log("Left trigger to accelerate");
                Debug.Log("Use Left Thumbstick to position driver");
            }
        }
        if (UIActive)
        {
            UI.transform.position = UIAnchor.transform.position;
            UI.transform.eulerAngles = new Vector3(UIAnchor.transform.eulerAngles.x, UIAnchor.transform.eulerAngles.y, 0);

        }
        //if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        //    Debug.Log("Right trigger pressed");
        //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        //    Debug.Log("Left trigger pressed");
        //if (OVRInput.GetUp(OVRInput.Touch.PrimaryThumbstick))
        //    Debug.Log("Left thumbstick moved up");
        //if (OVRInput.GetUp(OVRInput.Touch.SecondaryThumbstick))
        //    Debug.Log("Right thumbstick moved up");
    }
}
