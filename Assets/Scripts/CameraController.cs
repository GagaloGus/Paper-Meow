using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;
    public float speedSmooth;

    Vector2 maxSpeed;
    float originalYvalue;
    private void Start()
    {

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
            Debug.LogError("Camera object not found with tag 'FreeLook Camera'.");
        }
    }


    public void LockCamera()
    {
        if (cinemachine)
        {
            originalYvalue = cinemachine.m_YAxis.Value;

            
            StopAllCoroutines();
            StartCoroutine(SmoothYAxis(true, speedSmooth));

            cinemachine.m_XAxis.m_MaxSpeed = 0f;
            cinemachine.m_YAxis.m_MaxSpeed = 0f;
        }
    }

    public void ResetCamera()
    {
        if (cinemachine)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothYAxis(false, speedSmooth));
            cinemachine.m_XAxis.m_MaxSpeed = maxSpeed.x;
            cinemachine.m_YAxis.m_MaxSpeed = maxSpeed.y;

        }
    }

    IEnumerator SmoothYAxis(bool fadeIn, float speed)
    {
        if(fadeIn)
        {
            for(float i = originalYvalue; i >= 0; i -= speed)
            {
                cinemachine.m_YAxis.Value = i;
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i < originalYvalue; i += speed)
            {
                cinemachine.m_YAxis.Value = i;
                yield return null;
            }
        }
    }

    /*IEnumerator SmoothScreenX(bool fadeIn, float speed)
    {
        speed /= 20;
        if (fadeIn)
        {    
            for (float i = 0.5f; i >= 0.3f; i -= speed)
            {
                cinemachine.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = i;
                cinemachine.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = i;
                cinemachine.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = i;

                yield return null;
            }
        }
        else
        {
            for (float i = 0.3f; i < 0.5f; i += speed)
            {
                cinemachine.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = i;
                cinemachine.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = i;
                cinemachine.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = i;

                yield return null;
            }
        }
    }*/

    
}
