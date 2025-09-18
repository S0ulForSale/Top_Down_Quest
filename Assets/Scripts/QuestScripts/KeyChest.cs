using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChest : MonoBehaviour
{
    public GameObject keyObject; // Об'єкт ключа, який можна підняти
    public GameObject chestObject; // Об'єкт сховища ключа

    private bool isOpen = false; // Прапорець, який показує, чи вже сховище було відкрито

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen && other.CompareTag("Player"))
        {
            // Гравець зіткнувся з сховищем та може підняти ключ
            keyObject.SetActive(true); // Включаємо об'єкт ключа
            chestObject.SetActive(false); // Вимикаємо об'єкт сховища
            isOpen = true; // Встановлюємо прапорець, що сховище було відкрито
        }
    }
}
