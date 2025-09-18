using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossHPBar : MonoBehaviour
{   
    [SerializeField]
    private ManageBossPH manageBossPH;
    [SerializeField]
    private Image maxHealth;
    [SerializeField]
    private Image currentHealth;
    //[SerializeField]
    //private TMP_Text healthText;
    private float maxHealthValue;

    // Start is called before the first frame update
    void Start()
    {
        maxHealthValue = manageBossPH.maxHealth;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float healthPercentage = manageBossPH.currentHealth / maxHealthValue;
        currentHealth.fillAmount = healthPercentage;
        maxHealth.fillAmount = healthPercentage;

            // Оновити текстовий елемент здоров'я
        //healthText.text = manageBossPH.currentHealth.ToString() + " / " + manageBossPH.maxHealth.ToString();
    }
}
