using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook cinemachine;
    public GameObject pauseMenu;

    private void Start()
    {

        // Encuentra la cámara primero
        GameObject camObj = GameObject.FindWithTag("FreeLook Camera");

        if (camObj != null)
        {
            // Obtiene el componente CinemachineFreeLook
            cinemachine = camObj.GetComponent<CinemachineFreeLook>();

            if (cinemachine != null)
            {
                // Obtiene el componente CinemachineComposer
                CinemachineComposer comp = cinemachine.GetRig(1).GetCinemachineComponent<CinemachineComposer>();

                if (comp == null)
                {
                    Debug.LogError("CinemachineComposer component not found on the rig.");
                }
            }
            else
            {
                Debug.LogError("CinemachineFreeLook component not found on the camera object.");
            }
        }
        else
        {
            Debug.LogError("Camera object not found with tag 'FreeLook Camera'.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu != null)
            {
                LockCamera();
            }
            else
            {
                ResetCamera();
            }
        }
    }

    private void LockCamera()
    {
        if (cinemachine != null)
        {
            cinemachine.m_Lens.FieldOfView = 17;

            // Asegúrate de que el componente CinemachineComposer exista antes de acceder a sus propiedades
            CinemachineComposer comp = cinemachine.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
            if (comp != null)
            {
                comp.m_TrackedObjectOffset.x = -0.5f;
            }

            cinemachine.m_XAxis.m_MaxSpeed = 0f;
            cinemachine.m_YAxis.m_MaxSpeed = 0f;
        }
    }

    private void ResetCamera()
    {
        if (cinemachine != null)
        {
            cinemachine.m_Lens.FieldOfView = 40;

            // Asegúrate de que el componente CinemachineComposer exista antes de acceder a sus propiedades
            CinemachineComposer comp = cinemachine.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
            if (comp != null)
            {
                comp.m_TrackedObjectOffset.x = 0;
            }

            // Ajusta estos valores según lo necesario para restablecer la cámara
            cinemachine.m_XAxis.m_MaxSpeed = 300f;
            cinemachine.m_YAxis.m_MaxSpeed = 2f;
        }
    }
}
