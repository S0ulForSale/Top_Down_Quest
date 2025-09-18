using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damageToGive = 1;

    private HealthSystem healthSystem;

    private DemonControl demonControl;

    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        demonControl = GetComponent<DemonControl>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    // Перевіряємо, чи зіткнувся снаряд з об'єктом з тегом "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.DeadPlayer(damageToGive);

                // Створюємо ефект зіткнення та знищуємо снаряд
                Vector2 collisionPoint = collision.GetContact(0).point;
                Vector3 effectPosition = new Vector3(collisionPoint.x, collisionPoint.y, -1f);
                GameObject effect = Instantiate(hitEffect, effectPosition, Quaternion.identity);
                Destroy(effect, 0.2f);
                effect.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(collisionPoint.y, collisionPoint.x) * Mathf.Rad2Deg);
                Destroy(gameObject);
            }
        }
        // Перевіряємо, чи зіткнувся снаряд з іншим снарядом
        else if (!collision.gameObject.CompareTag("DemonBullet"))
        {
            // Якщо це не інший снаряд, створюємо ефект зіткнення та знищуємо снаряд
            Vector2 collisionPoint = collision.GetContact(0).point;
            Vector3 effectPosition = new Vector3(collisionPoint.x, collisionPoint.y, -1f);
            GameObject effect = Instantiate(hitEffect, effectPosition, Quaternion.identity);
            Destroy(effect, 0.2f);
            effect.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(collisionPoint.y, collisionPoint.x) * Mathf.Rad2Deg);
            Destroy(gameObject);
        }
    }


    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.tag == "Enemy")
    //     {
    //         Destroy(other.gameObject);
    //     }
    // }
}
