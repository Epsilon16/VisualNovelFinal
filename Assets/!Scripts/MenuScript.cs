using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private static MenuScript instance;

    public bool mouseControl;

    public GameObject OptionsCanvas;
    public GameObject RacineCanvas;
    private GameObject childOption;
   

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static MenuScript GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        DisableOption();
        //OptionsCanvas = GameObject.FindGameObjectsWithTag("Options");
    }

    public void LoadScene(int GameScene)
    {
        mouseControl = false;
        SceneManager.LoadScene(GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableOption()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (OptionsCanvas != null)
            OptionsCanvas.SetActive(true);
        else
        {
            //OptionsCanvas = GameObject.FindGameObjectWithTag("options");
            //Debug.Log(OptionsCanvas);
            RacineCanvas = GameObject.FindGameObjectWithTag("Options");
            childOption = RacineCanvas.transform.GetChild(0).gameObject;
            childOption.SetActive(true);

            if (DialogueManager.GetInstance())
            {
                DialogueManager.GetInstance().isOptionOn = true;
            }
        }
            //OptionsCanvas = GameObject.FindGameObjectWithTag("Options");      
    }

    public void DisableOption()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (OptionsCanvas != null)
            OptionsCanvas.SetActive(false);
        else
        {
            //OptionsCanvas = GameObject.FindGameObjectWithTag("options");
            RacineCanvas = GameObject.FindGameObjectWithTag("Options");
            childOption = RacineCanvas.transform.GetChild(0).gameObject;
            childOption.SetActive(false);
        }
    }
}
