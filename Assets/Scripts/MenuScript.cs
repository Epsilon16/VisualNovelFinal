using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
