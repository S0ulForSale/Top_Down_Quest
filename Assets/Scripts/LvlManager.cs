using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{ 
    private AudioSource audio;
    public AudioClip doorSound;
    
    public bool isLocked = true; // Прапорець, що показує, чи двері замкнуті

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLocked && collision.gameObject.CompareTag("Player"))
        {
            if (!isLocked) // Перевірка, чи двері не замкнуті
            {
                SceneController.instance.LoadNext(2);
                audio.PlayOneShot(doorSound);   
            }
            else
            {
                Debug.Log("Двері замкнуті. Лох!.");
            }
        }
    }
}