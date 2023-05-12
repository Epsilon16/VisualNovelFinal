using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame(int GameScene)
    {
        SceneManager.LoadScene(GameScene);
    }

    public void LoadGame(int GameScene)
    {
        Debug.Log(GameScene);
    }

    public void ReturnMenu(int GameScene)
    {
        SceneManager.LoadScene(GameScene);
    }

    public void CGScene(int GameScene)
    {
        Debug.Log(GameScene);
    }

    public void OptionMenu(int GameScene)
    {
        Debug.Log(GameScene);
    }

    public void QuitGame(int GameScene)
    {
        Application.Quit();
    }
}
