using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style = new GUIStyle();
    public int targetFPS;
    public int vsyncValue = 1;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = vsyncValue;
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Application.targetFrameRate != targetFPS)
        { Application.targetFrameRate = targetFPS; }
    }

    void OnGUI()
    {
        int fps = Mathf.RoundToInt(1.0f / deltaTime);
        string text = $"FPS: {fps}";

        // Configurar el estilo con un color personalizado
        style.normal.textColor = Color.red;

        // Mostrar el texto con el estilo personalizado
        GUI.Label(new Rect(10, 10, 100, 20), text, style);
    }

    public void ChangeFPSLimit(int num)
    {
        targetFPS = (num);
    }

    public void ToggleVSync()
    {
        // Alternar entre activado y desactivado
        vsyncValue = (vsyncValue == 0) ? 1 : 0;
        QualitySettings.vSyncCount = vsyncValue;

        Debug.Log($"VSync {(vsyncValue == 0 ? "desactivado" : "activado")}");
    }
}
