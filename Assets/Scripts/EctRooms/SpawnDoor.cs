using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoor : MonoBehaviour
{
    public Transform SpawnPointObject;
    public GameObject Prefab;
    private bool hasSpawned = false;
    private ManageBossPH bossManager;

    private void Start()
    {
        bossManager = FindObjectOfType<ManageBossPH>();

        // Підписатися на подію смерті "DemonBoss"
        bossManager.OnDemonBossDeath += HandleDemonBossDeath;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasSpawned)
        {
            Instantiate(Prefab, SpawnPointObject.position, SpawnPointObject.rotation);
            hasSpawned = true;
        }
    }

    // Метод, який буде викликаний при смерті "DemonBoss"
    private void HandleDemonBossDeath()
    {
        // Знищити створений об'єкт
        Destroy(Prefab);
    }

    // Важливо відписатися від події при знищенні об'єкта
    private void OnDestroy()
    {
        bossManager.OnDemonBossDeath -= HandleDemonBossDeath;
    }
}
