using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorObstaclesControl : MonoBehaviour
{
    public float rotationSpeed;

    private Transform rotationObject;

    private void Start()
    {
        rotationObject = this.gameObject.GetComponent<Transform>() as Transform;
    }
    void Update()
    {
        RotationObstacle();
    }
    void RotationObstacle()
    {
        rotationObject.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
