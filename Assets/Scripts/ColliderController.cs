using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public GameObject checkPoint;

    public float showerPointY;

    private void Update()
    {
        CharacterIsDrop();   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacles"))
        {
            CheckpointFrost();
        }
    }
    private void CheckpointFrost()
    {
        transform.position = checkPoint.transform.position;
    }
    private void CharacterIsDrop()
    {
        if (transform.position.y<showerPointY)
        {
            CheckpointFrost();
        }
    }
}
