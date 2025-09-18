using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthSystem playerHealth;
    [SerializeField]
    private Image maxHealth;
    [SerializeField]
    private Image currentHealth;
    [SerializeField]
    private TMP_Text healthText;
    private float maxHealthValue;

    // Start is called before the first frame update
    void Start()
    {
        maxHealthValue = playerHealth.maxHealth;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float healthPercentage = playerHealth.currentHealth / maxHealthValue;
        currentHealth.fillAmount = healthPercentage;
        maxHealth.fillAmount = healthPercentage;

            // Оновити текстовий елемент здоров'я
        healthText.text = playerHealth.currentHealth.ToString() + " / " + playerHealth.maxHealth.ToString();
    }
}
