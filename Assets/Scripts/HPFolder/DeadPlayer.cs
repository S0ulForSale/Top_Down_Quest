using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadPlayer : MonoBehaviour
{
    private HealthSystem healthSystem;
    private float WaitToHutr = 2f;
    //private bool reloading;
    private bool isTouching;

    [SerializeField]
    private int damageToGive = 1;



    // Start is called before the first frame update 
    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(reloading)
        // {
        //     WaitToLoad -= Time.deltaTime;
        //     if(WaitToLoad <= 0)
        //     {
        //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //     }
        // }   

        if(isTouching)
        {
            WaitToHutr -= Time.deltaTime;
            if(WaitToHutr <= 0)
            {
                healthSystem.DeadPlayer(damageToGive);
                WaitToHutr = 2f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            //Destroy(other.gameObject);
            // other.gameObject.SetActive(false);
            // reloading = true;
            other.gameObject.GetComponent<HealthSystem>().DeadPlayer(damageToGive);

            //reloading = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = false;
        }
    }
}
