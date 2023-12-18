using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style = new GUIStyle();

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
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
}
