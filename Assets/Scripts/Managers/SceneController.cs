using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class SceneController : MonoSingleton<SceneController>
    {
        [HideInInspector] public int currentScene;
        public bool isMenu => currentScene == 0;
        private Coroutine wait;
        
        public enum Scene
        {
            MainMenu,
            Lobby,
            Level1,
            Level2,
            Level3,
            End
        }

        public override void Init()
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
        }

        private void Cancel()
        {
            if (wait != null)
            {
                StopCoroutine(wait);
                wait = null;
            }
        }

        public void LoadScene(int sceneIdx,float delaySec = 0)
        {
            Cancel();
            if (delaySec > 0)
            {
                wait = StartCoroutine(this.WaitAndDo(
                    () =>
                    {
                        currentScene = sceneIdx;
                        SceneManager.LoadScene(currentScene);
                        wait = null;
                    }, delaySec));
                return;
            }

            currentScene = sceneIdx;
            SceneManager.LoadScene(currentScene);
        }

        public Scene GetCurrentScene()
        {
            if (currentScene == 0)
            {
                return Scene.MainMenu;
            }
            else if (currentScene == 1)
            {
                return Scene.Lobby;
            }
            else if(currentScene == 2)
            {
                return Scene.Level1;
            }
            if (currentScene == 3)
            {
                return Scene.Level2;
            }
            if (currentScene == 4)
            {
                return Scene.Level3;
            }
            else 
                return Scene.End;
        }

        public void LoadNext(float delaySec = 0)
        {
            LoadScene(currentScene+1);
        }
        
        public void Reload()
        {
            LoadScene(currentScene);
        }

        public void GoToMenu()
        {
            LoadScene(0);
        }
        
        public void GoToLobby()
        {
            LoadScene(2);
        }
    }
}