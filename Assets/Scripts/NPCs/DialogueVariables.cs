using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables {  get; private set; }

    Story globalVariableStory;

    const string saveVariablesKey = "INK_VARIABLES";

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        globalVariableStory = new Story(loadGlobalsJSON.text);

        if(PlayerPrefs.HasKey(saveVariablesKey))
        {
            string jsonSTate = PlayerPrefs.GetString(saveVariablesKey);
            globalVariableStory.state.LoadJson(jsonSTate);
        }

        //inicializa el diccionario
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariableStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariableStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log($"Variable global inicializada: {name} = {value}");
        }

    }

    public void StartListening(Story story)
    {
        //VariablesToStory debe ir antes que el listener
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    /*public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }*/

    void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log($"Variable cambiada: {name} = {value}");

        //Solo mantener variables inicializadas del archivo ink global
        if(variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable  in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);

        }
    }

    public void SaveVariables()
    {
        if(globalVariableStory != null)
        {
            VariablesToStory(globalVariableStory);

            //Hay que cambiar esto por un cargado/guardado funcional en vez de playerprefs
            PlayerPrefs.SetString(saveVariablesKey, globalVariableStory.state.ToJson());
        }
    }

}
