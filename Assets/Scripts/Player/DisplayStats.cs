using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
    public Transform statsMenu;
    public TMP_Text level_display;
    public TMP_Text stats_display;
    public TMP_Text exp_display;
    public Image weapon_display;

    public Slider expSlider;

    SkoStats playerStats;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onPauseMenuOpen += UpdateCanvasStats;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onPauseMenuOpen -= UpdateCanvasStats;
    }

    void Start()
    {
        statsMenu = transform.Find("Player Info");
        level_display = statsMenu.Find("Level").gameObject.GetComponent<TMP_Text>();
        stats_display = statsMenu.Find("Stats").gameObject.GetComponent<TMP_Text>();
        exp_display = statsMenu.Find("Exp").gameObject.GetComponent<TMP_Text>();
        weapon_display = statsMenu.Find("Weapon").gameObject.GetComponent<Image>();
        expSlider = statsMenu.gameObject.GetComponentInChildren<Slider>();

        UpdateCanvasStats();
    }

    public void UpdateCanvasStats()
    {
        playerStats = FindObjectOfType<SkoStats>();

        level_display.text = $"Lvl {playerStats.currentStats.currentLevel}";
        stats_display.text = $"ATK: {playerStats.currentStats.ATK} \nSPD: {playerStats.currentStats.SPD}";
        exp_display.text = $"XP: {playerStats.currentStats.EXP}/{playerStats.currentStats.EXP_RequiredNextLvl}";

        expSlider.value = CoolFunctions.MapValues(playerStats.currentStats.EXP, 0, playerStats.currentStats.EXP_RequiredNextLvl, 0, 1);

        weapon_display.sprite = InventoryManager.instance.quickSwapWeapons[0].GetComponentInChildren<InventoryItem>().item.sprite;
    }
}
