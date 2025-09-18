using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button mainMenu;
    [SerializeField] private Button resume;
    [SerializeField] private Button exit;
    public bool isOpen; //idk probably redundant
    
    public void Awake()
    {
        pauseButton.onClick.AddListener(() => SetOpen(true));
        resume.onClick.AddListener(() => SetOpen(false));
        exit.onClick.AddListener(QuitGame);
        mainMenu.onClick.AddListener(LoadMenu);
    }

    private void SetOpen(bool open)
    {
        isOpen = open;
        PauseManager.instance.SetPause(open);
        pauseMenuUI.SetActive(open);
    }

    private void LoadMenu()
    {
        SceneController.instance.GoToMenu();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}