using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float moveSpeed;
    public float mouseSpeed;

    Vector3 firstPos, endPos;

    private float difference;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SwerveMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            difference = endPos.x - firstPos.x;

            transform.Translate(difference * mouseSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetMouseButtonUp(0))
        {
            firstPos = Vector3.zero;
            endPos = Vector3.zero;
        }
    }

    public void ForwardMovement()
    {
        Vector3 position = this.transform.position;
        position.z += moveSpeed;
        this.transform.position = position;
    }
}
