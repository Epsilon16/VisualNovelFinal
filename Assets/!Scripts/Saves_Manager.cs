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
using Unity.VisualScripting;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

public class Saves_Manager : MonoBehaviour
{
    string[] savedSprites;
    string savedAudio;
    string savedMusic;
    string savedGlobals;
    string savedNext;
    string savedGSprite;
    
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

        savedAudio = "nothing";

        for (int i = 0; i < DialogueManager.GetInstance().audioInfos.Length; i++)
        {
            if (DialogueManager.GetInstance().currentAudioInfo == DialogueManager.GetInstance().audioInfos[i])
            {
                savedAudio = DialogueManager.GetInstance().currentAudioInfo.id + "";
                break;
            }
        }

        savedMusic = "nothing";
        if (DialogueManager.GetInstance().musicAS.clip != null)
        {
            savedMusic = DialogueManager.GetInstance().musicAS.clip.name;
        }

        savedGlobals = null;
        DialogueManager.GetInstance().dialogueVariables.VariablesToStory(DialogueManager.GetInstance().dialogueVariables.globalVariablesStory);
        savedGlobals = DialogueManager.GetInstance().dialogueVariables.globalVariablesStory.state.ToJson();

        savedNext = "nothing";
        if (DialogueManager.GetInstance().nextInkJSON != null)
        {
            savedNext = DialogueManager.GetInstance().nextInkJSON.name;
        }

        savedGSprite = "nothing";
        if (DialogueManager.GetInstance().gSprite != null)
        {
            savedGSprite = DialogueManager.GetInstance().gSprite.name;
        }

        return new SaveData
        {
            InkStoryState = DialogueManager.GetInstance().GetStoryState(),
            name = DialogueManager.GetInstance().displayNameText.text,
            background = DialogueManager.GetInstance().background.GetComponent<Image>().sprite.name,
            item = DialogueManager.GetInstance().item.GetComponent<Image>().sprite.name,
            itemstate = DialogueManager.GetInstance().itemset.ToString(),
            sprites = savedSprites,
            music = savedMusic,
            audio = savedAudio,
            globals = savedGlobals,
            currentjson = DialogueManager.GetInstance().firstInkJSON.name,
            nextjson = savedNext,
            grigristate = DialogueManager.GetInstance().wasactivated.ToString(),
            layoutstate = DialogueManager.GetInstance().isGrigriActivated.ToString(),
            gsprite = savedGSprite
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
    public string item;
    public string itemstate;
    public string[] sprites;
    public string music;
    public string audio;
    public string globals;
    public string currentjson;
    public string nextjson;
    public string grigristate;
    public string layoutstate;
    public string gsprite;
}
