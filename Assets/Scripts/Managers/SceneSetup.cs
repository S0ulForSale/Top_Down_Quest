using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers
{
    public class SceneSetup : MonoSingleton<SceneSetup>
    {
        private Transform player; // Посилання на гравця
        private Transform spawnPoint; // Позиція, де гравець повинен з'явитися

        public override void Init()
        {
            SceneManager.sceneLoaded += (_, _) => SetupPlayer();
        }

        private void SetupPlayer()
        {
            // Здійснюємо пошук гравця по тегу "Player"
            player = GameObject.FindGameObjectWithTag("Player").transform;

            // Здійснюємо пошук позиції спавну по тегу "SpawnPoint"
            spawnPoint = GameObject.FindGameObjectWithTag("SpawnCenter").transform;

            player.position = spawnPoint.position;
        }
    }
}