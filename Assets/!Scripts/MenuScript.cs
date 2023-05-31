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

    public GameObject CanvasOption;
   

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
        CanvasOption.SetActive(true);
        
    }

    public void DisableOption()
    {
        CanvasOption.SetActive(false);
        
    }
}
