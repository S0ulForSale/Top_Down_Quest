using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RangeAtkGobl : MonoBehaviour
{
    private HealthSystem healthSystem;
    public SlimeControl slimeControl;
    private bool isTouching;

    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public Transform firePoint;
    //public Transform firePoint2;

    [SerializeField]
    private int damageToGive = 1;

    private Transform playerTransform; // Позиція гравця 

    private bool isShooting = false;

    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ShootCoroutine());
    }

    void Update()
    {
        if (isTouching)
        {
            healthSystem.DeadPlayer(damageToGive);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            healthSystem.DeadPlayer(damageToGive);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isTouching = false;
        }
    }

    public IEnumerator ShootCoroutine()
    {
         while (true)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) <= slimeControl.area)
            {
                //Shoot();
                ShootType2();
            }
            yield return null;
        }
    }

    public void Shoot()
    {
        if (isShooting)
            return;

        isShooting = true;

        Vector2 direction = (playerTransform.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);

        Collider2D slimeCollider = GetComponent<Collider2D>();
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(slimeCollider, bulletCollider, true); // Ігнорувати колізію між колайдером кулі і колайдером об'єкта, до якого прикріплений firePoint 

        StartCoroutine(ResetShootFlag());
    }

    public void ShootType2()
    {
        if (isShooting)
            return;

            isShooting = true;

            Vector2 direction = (playerTransform.position - firePoint.position).normalized;

           // animator.SetTrigger("Attack");

            // Створюємо три снаряда
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            GameObject bullet3 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Задаємо різні сили відштовхування для кожного снаряда
            Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
            rb1.AddForce(direction * bulletForce, ForceMode2D.Impulse);

            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(Quaternion.Euler(0, 0, 15) * direction * bulletForce, ForceMode2D.Impulse);

            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            rb3.AddForce(Quaternion.Euler(0, 0, -15) * direction * bulletForce, ForceMode2D.Impulse);

            // Додайте обертання для кожного снаряда відповідно до його напрямку
            bullet1.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            bullet2.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2((Quaternion.Euler(0, 0, 15) * direction).y, (Quaternion.Euler(0, 0, 15) * direction).x) * Mathf.Rad2Deg);
            bullet3.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2((Quaternion.Euler(0, 0, -15) * direction).y, (Quaternion.Euler(0, 0, -15) * direction).x) * Mathf.Rad2Deg);

            Collider2D demonCollider = GetComponent<Collider2D>();
            Collider2D bulletCollider1 = bullet1.GetComponent<Collider2D>();
            Collider2D bulletCollider2 = bullet2.GetComponent<Collider2D>();
            Collider2D bulletCollider3 = bullet3.GetComponent<Collider2D>();

            // Ігноруємо колізію між колайдерами куль і колайдером об'єкта, до якого прикріплений firePoint 
            Physics2D.IgnoreCollision(demonCollider, bulletCollider1, true);
            Physics2D.IgnoreCollision(demonCollider, bulletCollider2, true);
            Physics2D.IgnoreCollision(demonCollider, bulletCollider3, true);

            StartCoroutine(ResetShootFlag());
    }

    private IEnumerator ResetShootFlag()
    {
        yield return new WaitForSeconds(1f); // Затримка між пострілами 
        isShooting = false;
    }

}

