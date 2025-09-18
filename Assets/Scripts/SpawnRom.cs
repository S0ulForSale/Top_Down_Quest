using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRom : MonoBehaviour
{
    public int openingDirection;
    private int rand;
    private bool spawned = false;
    private TemplateRooms templates;
    //public GameObject enemyPrefab; // Префаб моба

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<TemplateRooms>();
        Invoke("Spawn", 0.1f);
        
        //zSpawn = Vector3.zero;
        
    }

    void Spawn()
    {
        if(spawned == false)
        {

            if(openingDirection == 1)
            {
                //DownRoom
                rand = Random.Range(0, templates.DownRoom.Length);
                Instantiate(templates.DownRoom[rand], transform.position, templates.DownRoom[rand].transform.rotation);
            }
            else if(openingDirection == 2)
            {
                //TopRoom
                rand = Random.Range(0, templates.TopRoom.Length);
                Instantiate(templates.TopRoom[rand], transform.position, templates.TopRoom[rand].transform.rotation);
            }
            else if(openingDirection == 3)
            {
                //LeftRoom
                rand = Random.Range(0, templates.LeftRoom.Length);
                Instantiate(templates.LeftRoom[rand], transform.position, templates.LeftRoom[rand].transform.rotation);
            }
            else if(openingDirection == 4)
            {
                //RightRoom
                rand = Random.Range(0, templates.RightRoom.Length);
                Instantiate(templates.RightRoom[rand], transform.position, templates.RightRoom[rand].transform.rotation);
            }
            spawned = true;

            
        }
    }

    // void SpawnZomb()
    // {
    //     Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
    //     Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    // }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PoitSpawn"))
        {
            if(other.GetComponent<SpawnRom>().spawned == false && spawned == true)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
        Debug.Log("знищено");
    }
}
