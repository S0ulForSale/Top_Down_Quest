using UnityEngine;
using Utils;

namespace Managers
{
    public class PauseManager : MonoSingleton<PauseManager>
    {
        public bool gameIsPaused;
        
        public void TogglePause()
        {
            SetPause(!gameIsPaused);
        }

        public void SetPause(bool pause)
        {
            if (pause == gameIsPaused) return;
            if (pause)
            {
                Pause();
                return;
            }

            Resume();
        }

        private void Resume()
        {
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        private void Pause()
        {
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
    }
}