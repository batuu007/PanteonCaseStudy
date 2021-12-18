using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    public float turningPower;

    private Rigidbody rb;

    private void Start()
    {
        rb = transform.GetChild(0).GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(0, 0, turningPower));
    }
}
