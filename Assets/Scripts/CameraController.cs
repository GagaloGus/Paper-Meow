using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;
    public float speedSmooth;

    Vector2 maxSpeed;

    Animator animator;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onHouseEnterChange += HouseCamera;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onHouseEnterChange -= HouseCamera;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
        animator.enabled = false;
        
        // Encuentra la cámara primero
        cinemachine = GetComponent<CinemachineFreeLook>();
        CameraOriginalValues = new float[3];
        for (int i = 0; i < 3; i++)
        {
             CameraOriginalValues[i] = cinemachine.m_Orbits[i].m_Height;
        }

        if (cinemachine)
        {
            //cinemachine.m_Orbits[0].m_Height;
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
        GetComponent<CinemachineCollider>().enabled = true;

        if (cinemachine)
        {
            animator.SetBool("Paused", false);
            cinemachine.m_XAxis.m_MaxSpeed = maxSpeed.x;
            cinemachine.m_YAxis.m_MaxSpeed = maxSpeed.y;
            for (int i = 0; i < 3; i++)
            {
                cinemachine.m_Orbits[i].m_Height = CameraOriginalValues[i];
            }
        }
    } 

    public void HouseCamera(bool Inside)
    {
        if (Inside)
        {
            GetComponent<CinemachineCollider>().enabled = false;
            cinemachine.m_Orbits[0].m_Height = 20;
            cinemachine.m_Orbits[1].m_Height = 20;
            cinemachine.m_Orbits[2].m_Height = 20;
        }
        else
        {
            ResetCamera();
        }
        print(Inside);
    }
}
