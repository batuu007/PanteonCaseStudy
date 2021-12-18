using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineControl : MonoBehaviour
{
    public CinemachineVirtualCamera boyCam;
    public CinemachineVirtualCamera wallCam;

    private bool switchCam = true;

    public void SwitchCam()
    {
        if (switchCam)
        {
            boyCam.Priority = 0;
            wallCam.Priority = 1;
        }
        else
        {
            boyCam.Priority = 1;
            wallCam.Priority = 0;
        }
        switchCam = !switchCam;
    }
}
