using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class DisplayObtained : MonoBehaviour
{
    public GameObject textoObtenido;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onThingObtained += DisplayThingObtained;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onThingObtained -= DisplayThingObtained;
    }


    void DisplayThingObtained(string thingName)
    {
        GameObject texto = Instantiate(textoObtenido);
        texto.transform.SetParent(GameObject.FindGameObjectWithTag("ObtainedTextPlaceholder").transform);

        texto.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-7, 33), 0);
        texto.transform.localScale = Vector3.one;
        TMP_Text textotexto = texto.GetComponent<TMP_Text>();

        textotexto.text = $"Obtenido {thingName}";
    }
}
