using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite[] sprites;
    public int currenthealth;
    public int totalhealth;
    private Image health;
    private void Start()
    {
        health = GetComponent<Image>();
    }

    void Update()
    {
        float percentagehealth = (float) currenthealth / (float) totalhealth;

        health.sprite = sprites[(int)Math.Truncate((sprites.Length -1) * currenthealth/100d)];

        //GetComponent<RectTransform>().sizeDelta = new Vector2(percentagehealth * 100, 10);
    }
}
