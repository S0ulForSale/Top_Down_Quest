using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSkill : MonoBehaviour
{
    public float knockbackForce = 10f; // Сила відштовхування
    public float knockbackDuration = 0.2f; // Тривалість відштовхування

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Отримуємо напрямок від ворога до гравця
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

            // Застосовуємо відштовхування до гравця
            StartCoroutine(ApplyKnockback(knockbackDirection));
        }
    }

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
        // Вимикаємо колізію ворога з гравцем, щоб уникнути неправильних зіткнень
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), PlayerController.Instance.GetComponent<Collider2D>(), true);

        // Застосовуємо силу відштовхування до гравця
        rb.velocity = direction * knockbackForce;

        // Затримуємо виконання на тривалість відштовхування
        yield return new WaitForSeconds(knockbackDuration);

        // Включаємо колізію ворога з гравцем знову
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), PlayerController.Instance.GetComponent<Collider2D>(), false);

        // Зупиняємо рух ворога після відштовхування
        rb.velocity = Vector2.zero;
    }
}
