using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class QuestLocationTrigger : MonoBehaviour
{
    public Quest quest;
    public int itemAmount;
    public enum EventType { Collision, Event }
    public EventType eventType;
    public enum InteractType { DestroySelf, DisableInteractable, InvokeEvent, JustMarkerLol }
    public InteractType interactType;
    [HideInInspector] public UnityEvent interactEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.GetComponent<SkoController>() && eventType == EventType.Collision) && interactType != InteractType.JustMarkerLol)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (QuestManager.instance.AddQuestItem(quest, itemAmount))
        {
            if (interactType == InteractType.DestroySelf)
            {
                Destroy(gameObject);
            }
            else if (interactType == InteractType.DisableInteractable)
            {
                GetComponent<Interactable>().enabled = false;
            }
            else if (interactType == InteractType.InvokeEvent)
            {
                interactEvent?.Invoke();
            }

        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(QuestLocationTrigger))]
class QuestLocationEditor : Editor
{
    SerializedProperty myEventProp;
    
    void OnEnable()
    {
        // Obtenemos las referencias a las propiedades que queremos modificar
        myEventProp = serializedObject.FindProperty("interactEvent");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        // Actualizamos los valores de las propiedades
        serializedObject.Update();

        // Dependiendo del valor de myEnum, mostramos o no la propiedad myEvent
        QuestLocationTrigger script = (QuestLocationTrigger)target;
        if (script.interactType == QuestLocationTrigger.InteractType.InvokeEvent)
        {
            EditorGUILayout.PropertyField(myEventProp);
        }

        // Aplicamos los cambios a las propiedades
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
