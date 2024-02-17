using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;
    public float speedSmooth;

    Vector2 maxSpeed;
    float originalYvalue;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
        animator.enabled = false;
        
        // Encuentra la cámara primero
        cinemachine = GetComponent<CinemachineFreeLook>();

        if (cinemachine)
        {
            // Obtiene el componente CinemachineComposer
            CinemachineComposer comp = cinemachine.GetRig(2).GetCinemachineComponent<CinemachineComposer>();

            if (!comp)
            {
                Debug.LogError("CinemachineComposer component not found on the rig.");
            }

            maxSpeed.x = cinemachine.m_XAxis.m_MaxSpeed;
            maxSpeed.y = cinemachine.m_YAxis.m_MaxSpeed;
            
        }
        else
        {
            Debug.LogError("Camera object not found 'FreeLook Camera'.");
        }
    }

    public void LockCamera()
    {
        if(cinemachine)
        {
            cinemachine.m_XAxis.m_MaxSpeed = 0f;
            cinemachine.m_YAxis.m_MaxSpeed = 0f;
        }
    }


    public void PausedLockCamera()
    {
        if (cinemachine && !GameManager.instance.isInteracting)
        {
            animator.enabled = true;
            animator.SetBool("Paused", true);
            originalYvalue = cinemachine.m_YAxis.m_InputAxisValue;

            cinemachine.m_XAxis.m_MaxSpeed = 0f;
            cinemachine.m_YAxis.m_MaxSpeed = 0f;
        }
    }

    public void CHangeSpeedOfCamera(float speed)
    {
        if (cinemachine)
        {
            cinemachine.m_XAxis.m_MaxSpeed = speed;
        }
    }

    public void ResetCamera()
    {
        if (cinemachine)
        {
            animator.SetBool("Paused", false);
            cinemachine.m_XAxis.m_MaxSpeed = maxSpeed.x;
            cinemachine.m_YAxis.m_MaxSpeed = maxSpeed.y;

        }
    } 
}
