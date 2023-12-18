using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    GUIStyle style = new GUIStyle();
    public int targetFPS;
    public int vsyncValue = 1;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown fpsLimitDropdown;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = vsyncValue;
    }

    private void Start()
    {
        // Obtener las resoluciones disponibles
        Resolution[] resolutions = Screen.resolutions;

        // Limpiar opciones existentes en el dropdown
        resolutionDropdown.ClearOptions();

        // Lista para almacenar las opciones de texto
        List<string> options = new List<string>();

        // Agregar las resoluciones al dropdown
        foreach (Resolution res in resolutions)
        {
            string optionText = $"{res.width} x {res.height} {res.refreshRateRatio}Hz";
            options.Add(optionText);
        }

        // Agregar opciones al dropdown
        resolutionDropdown.AddOptions(options);

        // Configurar el método que se ejecutará cuando cambie la resolución
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);

        // Limpiar opciones existentes en el dropdown
        fpsLimitDropdown.ClearOptions();

        // Configurar opciones de límites de FPS
        List<string> fpsLimitOptions = new List<string> { "30", "60", "120", "144", "165", "Unlimited" };
        fpsLimitDropdown.AddOptions(fpsLimitOptions);

        // Configurar el método que se ejecutará cuando cambie el límite de FPS
        fpsLimitDropdown.onValueChanged.AddListener(ChangeFPSLimit);

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

    public void ChangeFPSLimit(int index)
    {
        string selectedOption = fpsLimitDropdown.options[index].text;

        if (selectedOption == "Unlimited")
        {
            targetFPS = -1; // Valor negativo para FPS ilimitado
        }
        else
        {
            targetFPS = int.Parse(selectedOption);
        }
    }

    public void ToggleVSync()
    {
        // Alternar entre activado y desactivado
        vsyncValue = (vsyncValue == 0) ? 1 : 0;
        QualitySettings.vSyncCount = vsyncValue;

        Debug.Log($"VSync {(vsyncValue == 0 ? "desactivado" : "activado")}");
    }
    public void ChangeResolution(int index)
    {
        // Obtener las resoluciones disponibles
        Resolution[] resolutions = Screen.resolutions;

        // Aplicar la resolución seleccionada
        if (index >= 0 && index < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[index];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }
}
