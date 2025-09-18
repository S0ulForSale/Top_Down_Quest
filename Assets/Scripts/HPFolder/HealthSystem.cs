using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;
    public PlayerStats playerStats;

    private Vector3 initialPosition; // Змінна для зберігання початкової позиції гравця

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        currentHealth = maxHealth;
        initialPosition = transform.position; // Збережемо початкову позицію гравця
    }

    public void DeadPlayer(int damageToGive)
    {
        currentHealth -= damageToGive;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            RespawnPlayer(); // Відновити гравця на початкову позицію
        }
    }

    private void RespawnPlayer()
    {
        // Перемістимо гравця на початкову позицію
        transform.position = initialPosition;
        SceneController.instance.GoToLobby();
    }
}
