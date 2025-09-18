using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateRooms : MonoBehaviour
{
    public GameObject[] TopRoom;
    public GameObject[] DownRoom;
    public GameObject[] LeftRoom;
    public GameObject[] RightRoom;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnFrees;
    public GameObject frees;
    public GameObject[] mobs;
    public GameObject key;
    

    void Update()
    {
        if (waitTime <= 0 && spawnFrees == false)
        {
            Instantiate(frees, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
            spawnFrees = true;
            SpawnKey();
            SpawnMobs();
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
        
    }

        void SpawnMobs()
        {
            foreach (GameObject room in rooms)
            {
                int numMobs = Random.Range(2, 8); // Випадкова кількість мобів
                
                List<Vector3> spawnPositions = new List<Vector3>(); // Список для збереження позицій спавну
                
                for (int i = 0; i < numMobs; i++)
                {
                    // Отримуємо випадкову точку в межах кімнати
                    Vector2 randomPoint = Random.insideUnitCircle * 4f; // Радіус в 2 одиниці (можете змінити на свій розсуд)
                    Vector3 spawnPosition = room.transform.position + new Vector3(randomPoint.x, randomPoint.y, 0f);

                    bool isValidSpawn = true;

                    // Перевіряємо відстань між новою позицією спавну та всіма попередніми позиціями спавну
                    foreach (Vector3 position in spawnPositions)
                    {
                        if (Vector3.Distance(spawnPosition, position) < 2f) // Мінімальна відстань між ворогами (можете змінити на свій розсуд)
                        {
                            isValidSpawn = false;
                            break;
                        }
                    }

                    if (isValidSpawn)
                    {
                        
                        // Додаємо позицію спавну до списку
                        spawnPositions.Add(spawnPosition);

                        GameObject randomMob = mobs[Random.Range(0, mobs.Length)];
                        Instantiate(randomMob, spawnPosition, Quaternion.identity);
                    }
                }
            }
        }

        void SpawnKey()
        {

         int randomRoomIndex = Random.Range(1, rooms.Count); // вібір випадкової кімнати
         Vector3 spawnPosition = rooms[randomRoomIndex].transform.position;

         Instantiate(key, spawnPosition, Quaternion.identity);

        }
    





    // масив та ліст кімнат
//     public GameObject[] TopRoom;
//     public GameObject[] DownRoom;
//     public GameObject[] LeftRoom;
//     public GameObject[] RightRoom;

//     public GameObject closedRoom;

//     public List<GameObject> rooms;

//     public float waitTime;
//     private bool spawnFrees;
//     public GameObject frees;
//     public GameObject mobs;
    

//     void Update()
//     {
//         if(waitTime <= 0 && spawnFrees == false)
//         {
//             Instantiate(frees, rooms[rooms.Count-1].transform.position, Quaternion.identity);
//             spawnFrees = true;
//             // for(int i = 0; i < rooms.Count; i++)
//             // {
//             //     if(i == rooms.Count-1){
//             //     Instantiate(frees, rooms[i].transform.position, Quaternion.identity);
//             //     spawnFrees = true;
//             //     }
//             // }
//             SpawnMobs();
//         }
//         else
//         {
//             waitTime -= Time.deltaTime;
//         }
//     }
//     void SpawnMobs()
//     {
//         foreach (GameObject room in rooms)
//         {
//             // Вибираємо випадковий міб з масиву mobs
//             //GameObject randomMob = mobs[Random.Range(0, mobs.Length)];

//             // Спавнимо обраного моба в кожній кімнаті
//             Instantiate(mobs, room.transform.position, Quaternion.identity);
//         }
// }

    
}
