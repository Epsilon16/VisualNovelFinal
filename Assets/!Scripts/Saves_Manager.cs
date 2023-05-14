using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Saves_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveGame()
    {
        SaveData save = CreateSaveGameObject();
        var bf = new BinaryFormatter();

        var savePath = Application.persistentDataPath + "/savedata.save";

        FileStream file = File.Create(savePath); // creates a file at the specified location

        bf.Serialize(file, save); // writes the content of SaveData object into the file

        file.Close();

        Debug.Log("Game saved");
    }

    private SaveData CreateSaveGameObject()
    {
        return new SaveData
        {
            InkStoryState = DialogueManager.GetInstance().GetStoryState(),
        };
    }

    public void LoadGame()
    {
        var savePath = Application.persistentDataPath + "/savedata.save";

        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(savePath, FileMode.Open);
            file.Position = 0;

            SaveData save = (SaveData)bf.Deserialize(file);

            file.Close();

            DialogueManager.LoadState(save.InkStoryState);

            MenuScript.GetInstance().LoadScene(1);
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}

[Serializable]
public class SaveData
{
    public string InkStoryState;
}
