using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RangeAttackDemon : MonoBehaviour
{
    private HealthSystem healthSystem;
    public DemonControl demonControl;
    public Animator animator;
    private bool isTouching;

    public GameObject bulletPrefab;
    public GameObject flamelashPrefab;
    public float flamelashTime = 0.55f; //тривалість життя flamelash (в секундах)
    public float bulletForce = 20f;
    public Transform firePoint;

    [SerializeField]
    private int damageToGive = 1;

    private Transform playerTransform; // Позиція гравця hitEffect

    private bool isShooting = false;

    void Start()
    {
        healthSystem = FindObjectOfType<HealthSystem>();
        animator = GetComponent<Animator>();
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
        if (other.CompareTag("DemonBullet"))
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
            if (Vector3.Distance(playerTransform.position, transform.position) <= demonControl.area)
            {
                float randomValue = Random.value; // Генеруємо випадкове число від 0 до 1

                if (randomValue < 0.2f) // Шанс випустити flamelashPrefab: 20% (0.2f)
                {
                    ShootFlamelash();
                }
                else
                {
                    Shoot();
                }
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

            animator.SetTrigger("Attack");

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

    public void ShootFlamelash()
    {
        if (isShooting)
            return;

        isShooting = true;

        Vector2 direction = (playerTransform.position - firePoint.position).normalized;

        // Створюємо flamelashPrefab
        GameObject flamelash = Instantiate(flamelashPrefab, firePoint.position, Quaternion.identity);

        // Задаємо силу відштовхування для flamelashPrefab
        Rigidbody2D rb = flamelash.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce * 0.5f, ForceMode2D.Impulse); // Зменшуємо силу відштовхування

        Collider2D demonCollider = GetComponent<Collider2D>();
        Collider2D flamelashCollider = flamelash.GetComponent<Collider2D>();

        // Ігноруємо колізію між колайдером flamelashPrefab та колайдером об'єкта, до якого прикріплений firePoint 
        Physics2D.IgnoreCollision(demonCollider, flamelashCollider, true);

        // Запускаємо корутину для знищення flamelash після закінчення анімації
        StartCoroutine(DestroyFlamelashAfterAnimation(flamelash));
        
    }
    private IEnumerator DestroyFlamelashAfterAnimation(GameObject flamelash)
    {
        // Отримуємо компонент аніматора з flamelash
        Animator flamelashAnimator = flamelash.GetComponent<Animator>();

        // Очікуємо до тих пір, поки анімація "ShootFlamelash" не завершиться
        yield return new WaitForSeconds(flamelashAnimator.GetCurrentAnimatorStateInfo(0).length);


        // Після закінчення анімації знищуємо об'єкт flamelash
        Destroy(flamelash);

        // Скидаємо прапорець isShooting
        isShooting = false;
    }

    private IEnumerator ResetShootFlag()
    {
        yield return new WaitForSeconds(1f); // Затримка між пострілами 
        isShooting = false;
    }

}

