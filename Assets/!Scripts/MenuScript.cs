using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private static MenuScript instance;

    private bool mouseControl;

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
        SceneManager.LoadScene(GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator SelectFirstChoice(GameObject selectedchoice)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(selectedchoice);

        if (mouseControl)
        {
            StartCoroutine(SelectFirstChoice(selectedchoice));
        }
    }

    public void Highlightchoice(GameObject thischoice)
    {
        StartCoroutine(SelectFirstChoice(thischoice));
        mouseControl = true;
    }
    public void StopHighlightchoice()
    {
        mouseControl = false;
    }
}