using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DashHUD : MonoBehaviour
{
   public Image dashImg;
    public float cooldown = 3.0f;
    // public KeyCode dashSpace;
    public Button dashCD;

    private PlayerController playerController;
    private float lastDashTime;
    private int currentDashCount;
    private int maxDashCount = 2; // Припустимо, що гравець може використовувати два рази Dash

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        dashImg.fillAmount = 0;
        lastDashTime = Time.time;
    }
    public void OnDashButtonClick()
    {
        StartCoroutine(StartDashCooldown()); // Запускаємо перезарядку при натисканні кнопки
    }

    private void Update()
    {
        // Перевірка наявності доступних Dash
        if (currentDashCount < maxDashCount)
        {
            if (dashCD.onClick != null && Time.time >= lastDashTime + cooldown)
            {
                lastDashTime = Time.time;
                currentDashCount++;
                //StartCoroutine(StartDashCooldown());
            }
        }
    }

    private IEnumerator StartDashCooldown()
    {
        float startTime = Time.time;

        // Зменшення заповнення зображення Dash під час перезарядки
        while (Time.time < startTime + cooldown)
        {
            float remainingTime = startTime + cooldown - Time.time;
            dashImg.fillAmount = 1 - (remainingTime / cooldown);
            yield return null;
        }

        dashImg.fillAmount = 0;
        currentDashCount = 0;
    }
}
