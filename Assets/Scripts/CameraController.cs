using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;
    public CinemachineCollider cinemachineCollider;
    public float speedSmooth;

    float[] CameraOriginalValues;
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

    private void Awake()
    {        
        animator = GetComponent<Animator>();
        cinemachine = GetComponent<CinemachineFreeLook>();
        cinemachineCollider = GetComponent<CinemachineCollider>();
    }
    private void Start()
    {
        animator.Play("Idle");
        animator.enabled = false;

        // Encuentra la cámara primero

        CameraOriginalValues = new float[3];
        for (int i = 0; i < 3; i++)
        {
            CameraOriginalValues[i] = cinemachine.m_Orbits[i].m_Height;
        }

        maxSpeed.x = cinemachine.m_XAxis.m_MaxSpeed;
        maxSpeed.y = cinemachine.m_YAxis.m_MaxSpeed;

    }

    public void LockCamera()
    {
        cinemachine.m_XAxis.m_MaxSpeed = 0f;
        cinemachine.m_YAxis.m_MaxSpeed = 0f;
    }


    public void PausedLockCamera()
    {
        if (!GameManager.instance.isInteracting)
        {
            animator.enabled = true;
            animator.SetBool("Paused", true);

            cinemachine.m_XAxis.m_MaxSpeed = 0f;
            cinemachine.m_YAxis.m_MaxSpeed = 0f;
        }
    }

    public void ChangeSpeedOfCamera(float speed)
    {
        cinemachine.m_XAxis.m_MaxSpeed = speed;

    }

    public void ResetCamera()
    {

        cinemachineCollider.enabled = true;
        animator.SetBool("Paused", false);
        cinemachine.m_XAxis.m_MaxSpeed = maxSpeed.x;
        cinemachine.m_YAxis.m_MaxSpeed = maxSpeed.y;
        for (int i = 0; i < 3; i++)
        {
            cinemachine.m_Orbits[i].m_Height = CameraOriginalValues[i];
        }

    }

    public void HouseCamera(bool Inside)
    {
        if (Inside)
        {
            cinemachineCollider.enabled = false;
            cinemachine.m_Orbits[0].m_Height = 20;
            cinemachine.m_Orbits[1].m_Height = 20;
            cinemachine.m_Orbits[2].m_Height = 20;
        }
        else
        {
            ResetCamera();
        }
    }
}