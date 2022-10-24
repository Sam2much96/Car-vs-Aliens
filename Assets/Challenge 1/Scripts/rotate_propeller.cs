using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_propeller : MonoBehaviour
{
 

    // Update is called once per frame
    public float turnSpeed= 200.9f;
    public Vector3 offset;
    void Update()
    {
        transform.Rotate(offset * turnSpeed * Time.deltaTime );
    }
}
