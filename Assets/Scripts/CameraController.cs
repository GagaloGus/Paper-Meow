using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;
    public CinemachineCollider cinemachineCollider;

    [Header("Camera Y movement")]
    public float mouseY_Value;
    public float mouseY_Input;
    public bool invertYAxis;
    public float maxYSpeed;
    public Vector2 clampYcamera;

    float cameraHeight_originalValue;
    Vector3 originalSpeeds; //X es en x, Y es en Y, Z es scroll

    [Header("Build")]
    public Vector3 buildSpeeds;
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

        if(!Application.isEditor) //Si no es el editor (o sea, es la aplicacion buildeada) le pone los valores que necesita
        {
            cinemachine.m_XAxis.m_MaxSpeed = buildSpeeds.x;
            maxYSpeed = buildSpeeds.y;
            cinemachine.m_YAxis.m_MaxSpeed = buildSpeeds.z;
        }


        cameraHeight_originalValue = 1.33f;
        originalSpeeds.x = cinemachine.m_XAxis.m_MaxSpeed;
        originalSpeeds.y = maxYSpeed;
        originalSpeeds.z = cinemachine.m_YAxis.m_MaxSpeed;
    }


    private void FixedUpdate()
    {
        Control_CameraHeight(); 
    }

    void Control_CameraHeight()
    {
        mouseY_Input = Input.GetAxis("Mouse Y");

        if(Mathf.Abs(mouseY_Input) > 0.05f)
        {
            mouseY_Value += mouseY_Input * (maxYSpeed/100) * (invertYAxis ? -1 : 1);
            mouseY_Value = Mathf.Clamp(mouseY_Value, clampYcamera.x, clampYcamera.y);
        }

        for (int i = 0; i < 3; i++)
        {
            cinemachine.m_Orbits[i].m_Height = mouseY_Value;
        }
    }

    public void LockCamera()
    {

        cinemachine.m_XAxis.m_MaxSpeed = 0f;
        maxYSpeed = 0f;
        cinemachine.m_YAxis.m_MaxSpeed = 0f;
    }


    public void PausedLockCamera()
    {
        if (!GameManager.instance.isInteracting)
        {
            animator.enabled = true;
            animator.SetBool("Paused", true);

            cinemachine.m_XAxis.m_MaxSpeed = 0f;
            maxYSpeed = 0f;
            cinemachine.m_YAxis.m_MaxSpeed = 0f;
        }
    }

    public void ChangeSpeedOfCamera(float speed)
    {
        cinemachine.m_XAxis.m_MaxSpeed = speed;
        maxYSpeed = speed/15;
        cinemachine.m_YAxis.m_MaxSpeed = speed/10;

    }

    public void ResetCamera()
    {

        cinemachineCollider.enabled = true;
        animator.SetBool("Paused", false);
        cinemachine.m_XAxis.m_MaxSpeed = originalSpeeds.x;
        maxYSpeed = originalSpeeds.y;
        cinemachine.m_YAxis.m_MaxSpeed = originalSpeeds.z;


    }

    public void HouseCamera(bool Inside)
    {
        if (Inside)
        {
            cinemachineCollider.enabled = false;
            for (int i = 0; i < 2; i++)
            {
                cinemachine.m_Orbits[i].m_Height = 20;
            }
        }
        else
        {
            ResetCamera();
        }
    }
}