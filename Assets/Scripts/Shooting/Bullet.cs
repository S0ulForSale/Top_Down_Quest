using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType _bulletType;

    public GameObject hitEffect;
    public int damageToGive = 1;
    public float timeToLive = 10;

    private PlayerStats playerStats;


    public enum BulletType
    {
        Default,
        NonDefault
    }

    public void FixedUpdate()
    {
        timeToLive -= Time.fixedDeltaTime;
        if (timeToLive > 0) return;
        var pos = transform.position;
        GameObject effect = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(effect, 0.2f);
        effect.transform.rotation =
            Quaternion.Euler(0f, 0f, Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg);
        Destroy(gameObject);
    }
    
    

    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("DemonBoss"))
        {
            ManageEnemyPH manageEnemyPH = collision.gameObject.GetComponent<ManageEnemyPH>();
            ManageBossPH manageBossPH = collision.gameObject.GetComponent<ManageBossPH>();
            if (manageEnemyPH != null)
            {
                manageEnemyPH.HitEnemy(damageToGive);
            }

            if (manageBossPH != null)
            {
                manageBossPH.HitEnemy(damageToGive, _bulletType);
            }
        }

        Vector2 collisionPoint = transform.position;
        if (collision.contacts.Length != 0)
            collisionPoint = collision.GetContact(0).point;
        GameObject effect = Instantiate(hitEffect, collisionPoint, Quaternion.identity);
        Destroy(effect, 0.2f);
        effect.transform.rotation =
            Quaternion.Euler(0f, 0f, Mathf.Atan2(collisionPoint.y, collisionPoint.x) * Mathf.Rad2Deg);
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