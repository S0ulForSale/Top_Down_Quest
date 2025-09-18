using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneController.instance.GoToLobby();
    }

    public void QuitGame()
    {
        Debug.Log("ВИХІД!");
        Application.Quit();
    }
}
