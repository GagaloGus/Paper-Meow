using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public Sprite[] sprites;
    //public int currenthealth;
    //public int totalhealth;
    private Image health;
    private void Start()
    {
        health = GetComponent<Image>();
    }

    void Update()
    {
        //me invente esta forma temporalmente pq no entiendo muy bien la tuya
        float temp = CoolFunctions.MapValues(GameManager.instance.health, 0, GameManager.instance.maxHealth, 0, sprites.Length - 1);
        int healthPer = (int)Math.Truncate((double)temp);
        healthPer = Mathf.Abs(healthPer);

        health.sprite = sprites[healthPer];

        //float percentagehealth = (float) GameManager.instance.health / (float) GameManager.instance.maxHealth;

        //health.sprite = sprites[(int)Math.Truncate((sprites.Length -1) * GameManager.instance.health / 100d)];

        //GetComponent<RectTransform>().sizeDelta = new Vector2(percentagehealth * 100, 10);
    }
}
