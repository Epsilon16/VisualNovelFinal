
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public Story globalVariablesStory;

    public DialogueVariables (TextAsset loadGlobalsJSON)
    {
        //load le dossier global
        globalVariablesStory = new Story(loadGlobalsJSON.text);
        if (DialogueManager.loadedState != null)
        {
            globalVariablesStory.state.LoadJson(DialogueManager.loadedState.globals);
        }

        //créé un dictionnaire enregistrant les globals
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    //change en temps réel les variables
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    //arrête de changer les variables
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    //Change the variable inside Dictionnary
    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        Debug.Log("Variable changed: " + name + " = " + value);
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    //???
    public void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
