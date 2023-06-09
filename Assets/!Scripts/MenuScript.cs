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
    private GameObject childOption;

    public GameObject optionMenuPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        OptionsCanvas = Instantiate(optionMenuPrefab, transform.position, transform.rotation);
    }

    public static MenuScript GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        childOption = OptionsCanvas.transform.GetChild(0).gameObject;
        DisableOption();
    }

    private void Update()
    {
        /*if (InputManager.GetInstance().GetMenuPressed() && DialogueManager.GetInstance().isOptionOn)
        {
            DisableOption();
        }*/
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
        childOption.SetActive(true);

        if (DialogueManager.GetInstance())
        {
            DialogueManager.GetInstance().isOptionOn = true;
        }
    }

    public void DisableOption()
    {
        EventSystem.current.SetSelectedGameObject(null);
        childOption.SetActive(false);
    }
}
