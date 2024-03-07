using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestWaypoint : MonoBehaviour
{
    public Image image;
    public Transform target;
    public Vector3 offset;
    TMP_Text text;
    GameObject player;

    public List<QuestLocationTrigger> questObjects;

    private void OnEnable()
    {
        GameEventsManager.instance.npcEvents.onFollowQuest += ShowMarker;
        GameEventsManager.instance.npcEvents.onFinishedQuest += EraseMarkerList;
        GameEventsManager.instance.npcEvents.onUnfollowQuest += HideMarker;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.npcEvents.onFollowQuest -= ShowMarker;
        GameEventsManager.instance.npcEvents.onFinishedQuest -= EraseMarkerList;
        GameEventsManager.instance.npcEvents.onUnfollowQuest -= HideMarker;
    }

    void Start()
    {
        player = FindObjectOfType<SkoController>().gameObject;  
        text = image.GetComponentInChildren<TMP_Text>();
        questObjects = FindObjectsByType<QuestLocationTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();

        image.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (image.gameObject.activeInHierarchy)
        {
            float minX = image.GetPixelAdjustedRect().width/2;
            float minY = image.GetPixelAdjustedRect().height/2;
            float maxX = Screen.width - minX;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

            if(Vector3.Dot(target.position - Camera.main.transform.position, Camera.main.transform.forward)< 0)
            {
                //Marcador esta detras del player
                pos.x = pos.x < Screen.width / 2 ? maxX : minX;
            }

            pos = new Vector2(
                Mathf.Clamp(pos.x, minX, maxX),
                Mathf.Clamp(pos.y, minY, maxY)
                );

            image.transform.position = pos;
            text.text = $"{Mathf.RoundToInt(Vector3.Distance(player.transform.position, target.position))}m";
        }
    }

    QuestLocationTrigger questObject = null;
    public void ShowMarker(Quest quest)
    {
        questObject = Array.Find(questObjects.ToArray(), x => x.quest == quest);
        if(questObject != null )
        {
            target = questObject.transform;
            image.gameObject.SetActive(true);
        }
    }

    void EraseMarkerList(Quest quest)
    {
        if(questObject != null)
        {
            questObjects.Remove(questObject);
        }
    }

    public void HideMarker()
    {
        image.gameObject.SetActive(false);
    }
}
