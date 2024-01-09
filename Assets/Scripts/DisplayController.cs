using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
    float deltaTime = 0.0f;
    float accum = 0.0f; // Suma acumulada de los FPS
    int frames = 0; // Numero de frames en el intervalo
    float timeleft; // Tiempo restante antes de actualizar el promedio de FPS
    GUIStyle style = new GUIStyle();
    public int targetFPS;
    public int vsyncValue = 0;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown fpsLimitDropdown;
    public Slider brightnessSlider;

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

        brightnessSlider.value = GetBrightness();

        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);

        // Inicializar el tiempo restante para el cálculo del promedio de FPS
        timeleft = 0.5f;
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calcular el FPS actual y sumarlo a la acumulación
        float fps = 1.0f / Time.deltaTime;
        accum += fps;
        frames++;

        if (Application.targetFrameRate != targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }

        // Actualizar el tiempo restante
        timeleft -= Time.deltaTime;

        // Cuando el intervalo de tiempo ha pasado, reiniciar la acumulación y el contador de frames
        if (timeleft <= 0.0f)
        {
            accum = 0.0f;
            frames = 0;
            timeleft = 0.5f; // Establecer el intervalo de tiempo para el próximo cálculo del promedio
        }
    }

    void OnGUI()
    {
        // Configurar el estilo con un color personalizado
        style.normal.textColor = Color.red;

        // Calcular el promedio de FPS
        float averageFPS = accum / frames;
        string text = $"FPS: {Mathf.RoundToInt(1.0f / deltaTime)}\nAverage FPS: {Mathf.RoundToInt(averageFPS)}";

        // Mostrar el texto con el estilo personalizado
        GUI.Label(new Rect(10, 10, 200, 40), text, style);
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
    public void ToggleFulssScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    float GetBrightness()
    {
        return Screen.brightness;
    }
    public void ChangeBrightness(float value)
    {
        Screen.brightness = value;
    }
}
