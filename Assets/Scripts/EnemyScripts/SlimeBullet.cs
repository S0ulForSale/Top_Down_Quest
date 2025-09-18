using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damageToGive = 1;

    //private PlayerStats playerStats;
    private HealthSystem healthSystem;

    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        //playerStats = PlayerStats.Instance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.DeadPlayer(damageToGive);
            }
        }

        Vector2 collisionPoint = collision.GetContact(0).point;
        GameObject effect = Instantiate(hitEffect, collisionPoint, Quaternion.identity);
        Destroy(effect, 0.2f);
        effect.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(collisionPoint.y, collisionPoint.x) * Mathf.Rad2Deg);
        Destroy(gameObject);
    }


    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.tag == "Enemy")
    //     {
    //         Destroy(other.gameObject);
    //     }
    // }
}
