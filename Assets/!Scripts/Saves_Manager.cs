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
    string[] savedSprites;

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
        savedSprites = null;
        savedSprites = new string[DialogueManager.GetInstance().spritePlacement.transform.childCount];

        for (int i = 0; i < DialogueManager.GetInstance().spritePlacement.transform.childCount; i++)
        {
            if (DialogueManager.GetInstance().spritePlacement.transform.GetChild(i).GetComponent<Image>().color == Color.white)
            {
                savedSprites[i] = DialogueManager.GetInstance().spritePlacement.transform.GetChild(i).GetComponent<Image>().sprite.name;
            }
            else
            {
                savedSprites[i] = null;
            }

        }

        return new SaveData
        {
            InkStoryState = DialogueManager.GetInstance().GetStoryState(),
            name = DialogueManager.GetInstance().displayNameText.text,
            background = DialogueManager.GetInstance().background.GetComponent<Image>().sprite.name,
            sprites = savedSprites,
            //audioclip
            //audio = DialogueManager.GetInstance().currentAudioInfo,
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

            DialogueManager.LoadState(save);

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
    public string name;
    public string background;
    public string[] sprites;
    public string music;
    public string audio;
    //les globals
}
