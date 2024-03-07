using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite[] sprites;
    private Image health;

    [Header("Temblor")]
    public float velocidadVertical; // Velocidad de la oscilación vertical
    public float velocidadHorizontal; // Velocidad de la oscilación horizontal
    public float distanciaVertical; // Amplitud del temblor vertical
    public float distanciaHorizontal; // Amplitud del temblor horizontal
    Vector2 posicionOriginal;

    [Range(0f, 26)]
    public int startTremblePercentage;

    private void Start()
    {
        health = transform.Find("Healthbar").GetComponent<Image>();
        UpdateHealth();
        posicionOriginal = health.gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onHealthUpdate += UpdateHealth;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onHealthUpdate -= UpdateHealth;
    }

    private void Update()
    {
        if(GameManager.instance.health <= startTremblePercentage)
        {
            Temblar();
        }

        //UpdateHealth();
    }

    void UpdateHealth()
    {
        float temp = CoolFunctions.MapValues(GameManager.instance.health, 0, GameManager.instance.maxHealth, sprites.Length - 1, 0);
        int healthPer = (int)Math.Truncate((double)temp);
        healthPer = Mathf.Abs(healthPer);

        health.sprite = sprites[healthPer];
    }

    void Temblar()
    {
        float multiplier = CoolFunctions.MapValues(GameManager.instance.health, 0, startTremblePercentage, 50, 1);
        // Generar una oscilación en la posición del sprite utilizando Mathf.Sin
        float desplazamientoVertical = distanciaVertical/100 * Mathf.Sin(Time.time * velocidadVertical) * multiplier;
        float desplazamientoHorizontal = distanciaHorizontal/100 * Mathf.Sin(Time.time * velocidadHorizontal) * multiplier;

        // Aplicar la nueva posición al RectTransform del sprite
        health.gameObject.GetComponent<RectTransform>().anchoredPosition = posicionOriginal + new Vector2(desplazamientoHorizontal, desplazamientoVertical);
    }
}
