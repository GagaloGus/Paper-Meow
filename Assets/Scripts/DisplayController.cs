using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    public TMPro.TMP_Dropdown qualityDropdown;
    public TMPro.TMP_Dropdown filtersDropdown;
    public SOG.CVDFilter.CVDFilter cvdFilter;
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
            string optionText = $"{res.width} x {res.height}";
            options.Add(optionText);
        }

        // Agregar opciones al dropdown
        resolutionDropdown.AddOptions(options);

        // Configurar el m�todo que se ejecutar� cuando cambie la resoluci�n
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);

        // Limpiar opciones existentes en el dropdown
        fpsLimitDropdown.ClearOptions();

        // Configurar opciones de l�mites de FPS
        List<string> fpsLimitOptions = new List<string> { "30", "60", "120", "144", "165", "Unlimited" };
        fpsLimitDropdown.AddOptions(fpsLimitOptions);

        // Configurar el m�todo que se ejecutar� cuando cambie el l�mite de FPS
        fpsLimitDropdown.onValueChanged.AddListener(ChangeFPSLimit);

        brightnessSlider.value = GetBrightness();

        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);

        // Inicializar el tiempo restante para el c�lculo del promedio de FPS
        timeleft = 0.5f;

        // Aseg�rate de que el Dropdown tenga opciones configuradas en el Editor de Unity
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());

        // Asigna el evento de cambio del Dropdown a la funci�n SetQuality
        qualityDropdown.onValueChanged.AddListener(SetQuality);

        // Asigna la funci�n de cambio al evento onValueChanged del Dropdown
        filtersDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        // Configura las opciones del Dropdown
        filtersDropdown.ClearOptions();
        filtersDropdown.AddOptions(System.Enum.GetNames(typeof(SOG.CVDFilter.VisionTypeNames)).ToList());

        // Llama a la funci�n para inicializar el filtro con la opci�n actual del Dropdown
        OnDropdownValueChanged(filtersDropdown.value);

    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calcular el FPS actual y sumarlo a la acumulaci�n
        float fps = 1.0f / Time.deltaTime;
        accum += fps;
        frames++;

        if (Application.targetFrameRate != targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }

        // Actualizar el tiempo restante
        timeleft -= Time.deltaTime;

        // Cuando el intervalo de tiempo ha pasado, reiniciar la acumulaci�n y el contador de frames
        if (timeleft <= 0.0f)
        {
            accum = 0.0f;
            frames = 0;
            timeleft = 0.5f; // Establecer el intervalo de tiempo para el pr�ximo c�lculo del promedio
        }
    }

    void OnGUI()
    {
        
        // Configurar el estilo con un color personalizado
        style.normal.textColor = Color.red;

        // Calcular el promedio de FPS
        float averageFPS = accum / frames;
        string text = $"FPS: {Mathf.RoundToInt(1.0f / deltaTime)}\n" +
            $"Average FPS: {Mathf.RoundToInt(averageFPS)}\n" +
            $"------ GameManager Variables ------ \n" +
            $"GamePaused: {GameManager.instance.gamePaused} \n IsInteracting: {GameManager.instance.isInteracting} \n IsTutorial: {GameManager.instance.isTutorial} \n" +
            $"Player: {GameManager.instance.player.name} \n" +
            $"------ Misc Variables ------\n" +
            $"Cursor: Lock->{Cursor.lockState}, Visible->{Cursor.visible}\n";

        SkoController playerCont = FindObjectOfType<SkoController>();
        if (playerCont != null)
        {
            text += $"------ Player ------\n" +
            $"Can Move: {playerCont.player_canMove}\n Is Grounded: {playerCont.player_isGrounded}\n" +
            $"Skill: {playerCont.gameObject.GetComponent<SkillManager>().skillUsed}";
        }

        // Mostrar el texto con el estilo personalizado
        GUI.Label(new Rect(Screen.width-400, 10, 400, 400), text, style);
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

        // Aplicar la resoluci�n seleccionada
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
    // Funci�n llamada cuando cambia la selecci�n del Dropdown
    void SetQuality(int qualityIndex)
    {
        // Establece la calidad de gr�ficos seg�n el �ndice seleccionado en el Dropdown
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    void OnDropdownValueChanged(int index)
    {
        // Cambia el tipo de visi�n en el CVDFilter seg�n la opci�n seleccionada en el Dropdown
        cvdFilter.currentType = (SOG.CVDFilter.VisionTypeNames)index;
        // Llama a la funci�n para cambiar el perfil del filtro
        cvdFilter.ChangeProfile();
    }
}
