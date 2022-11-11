using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringMovement : MonoBehaviour
{
    public float speed;
    
    // Called before the first frame update
    void Start()
    {
    }

    // Called once per frame
    private void Update()
    {

    if (Input.GetKey(KeyCode.A))
        transform.Rotate(Vector3.down * speed * Time.deltaTime);
      
    if (Input.GetKey(KeyCode.D))
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}