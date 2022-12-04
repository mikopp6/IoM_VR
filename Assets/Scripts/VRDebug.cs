using UnityEngine;

public class VRDebug : MonoBehaviour
{
    // https://www.youtube.com/watch?v=h0OGk4fwVDg

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
        }
        if (UIActive)
        {
            UI.transform.position = UIAnchor.transform.position;
            UI.transform.eulerAngles = new Vector3(UIAnchor.transform.eulerAngles.x, UIAnchor.transform.eulerAngles.y, 0);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            Debug.Log("Right trigger pressed");
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            Debug.Log("Left trigger pressed");
        if (OVRInput.GetUp(OVRInput.Touch.PrimaryThumbstick))
            Debug.Log("Left thumbstick moved up");
        if (OVRInput.GetUp(OVRInput.Touch.SecondaryThumbstick))
            Debug.Log("Right thumbstick moved up");
    }
}
