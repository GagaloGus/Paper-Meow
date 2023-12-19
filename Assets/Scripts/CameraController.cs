using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;

    private void Start()
    {
        cinemachine.m_Lens.FieldOfView = 17;
    }
}
