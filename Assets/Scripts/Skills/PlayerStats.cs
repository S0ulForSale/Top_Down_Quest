using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerStats : MonoBehaviour
{
    // public string skillName;
    // public Sprite skillSprite;
    // [TextArea(1, 3)]
    // public string skillDels;
    // public PlayerStats[] skills;upgradePointsText
    // public bool isUpgrade;upgradePointsText
    public int healthUpgradeAmount = 5;
    [SerializeField]
    public static PlayerStats Instance;
    public delegate void ExpChangeHandler(int amount);
    public event ExpChangeHandler OnExpChange;

    public float moveSpeed;
    public int damage;
    public int maxHealth;
    

    [SerializeField]
    private int currentExpiriance;
    [SerializeField]
    private int maxExpiriance;
    [SerializeField]
    public int currentLevel;
    [SerializeField]
    private int upgradePoints = 0;
    //________________________________

    public Image expBar;
    public TMP_Text levelText;
    public TMP_Text expText;
    public TMP_Text upgradePointsText;
    //________________________________

    public GameObject upgradeMenu;
    public TMP_Text upgradeTitle;
    public TMP_Text upgradeDescription;
    public TMP_Text upgradePointsDisplay;
    public Button openUpgradeMenu;
    public Button closeUpgradeMenu;
    public Button upgradeSpeedButton;
    public Button upgradeDamageButton;
    public Button upgradeHealthButton;
    public Button confirmUpgradeButton;
    //________________________________
    
    private bool isUpgradeMenuOpen = false; // Прапорець, який показує, чи відкрите меню вдосконалення
    private UpgradeType selectedUpgrade; // Вибране вдосконалення

    public enum UpgradeType
    {
        Speed,
        Damage,
        Health
    }
    

    void Awake()
    {
        if (Instance == null) // Перевірка на те, чи існує вже екземпляр PlayerStats
        {
            Instance = this; // Якщо немає, то створюємо його
            DontDestroyOnLoad(gameObject); // Таким чином, екземпляр буде жити на протязі усієї гри і буде доступний з інших сцен
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Встановлюємо налаштування для відображення інформації про досвід та вдосконалення
        if (expBar != null && levelText != null && expText != null && upgradePointsText != null)
        {
            UpdateExperienceUI();
            UpdateLevelUI();
            UpdateUpgradePointsUI();
        }

         // Налаштовуємо кнопки та обробники подій для меню вдосконалення
        if (upgradeMenu != null && upgradeSpeedButton != null && upgradeDamageButton != null && upgradeHealthButton != null && confirmUpgradeButton != null)
        {
            upgradeMenu.SetActive(false);

            // Налаштування обробників подій для кнопок
            upgradeSpeedButton.onClick.AddListener(SelectUpgradeSpeed);
            upgradeDamageButton.onClick.AddListener(SelectUpgradeDamage);
            upgradeHealthButton.onClick.AddListener(SelectUpgradeHealth);
            confirmUpgradeButton.onClick.AddListener(ConfirmUpgrade);
        }
        if (upgradeMenu != null && openUpgradeMenu != null && closeUpgradeMenu != null) // Перевіряємо, що openUpgradeMenu не null
        {
            upgradeMenu.SetActive(false);

            // Додаємо обробник події для кнопки openUpgradeMenu
            openUpgradeMenu.onClick.AddListener(OpenUpgradeMenu);
            closeUpgradeMenu.onClick.AddListener(CloseUpgradeMenu); // Викликаємо метод ToggleUpgradeMenu()
        }

        isUpgradeMenuOpen = false;

    }

    private void ToggleUpgradeMenu()
    {
         if (isUpgradeMenuOpen)
            CloseUpgradeMenu();
        else
            OpenUpgradeMenu();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
    {
        ToggleUpgradeMenu(); // Викликаємо метод ToggleUpgradeMenu()
    }
        // Коли натиснута клавіша "U", відкриваємо або закриваємо меню вдосконалення
        // if (Input.GetKeyDown(KeyCode.U) || openUpgradeMenu.onClick)
        // {
        //     if (isUpgradeMenuOpen)
        //         CloseUpgradeMenu();
        //     else
        //         OpenUpgradeMenu();
        // }
    }

    public void GainExperience(int amount)
    {
        currentExpiriance += amount;
        if (currentExpiriance >= maxExpiriance)
        {
            LevelUp();
        }

        UpdateExperienceUI();
    }

    public void LevelUp()
    {
        // Збільшуємо рівень гравця, додаємо бал вдосконалення, обнуляємо досвід та збільшуємо максимальний досвід для наступного рівня
        currentLevel++;
        upgradePoints++;
        currentExpiriance = 0;
        maxExpiriance += 100;

        if (levelText != null)
        {
            UpdateLevelUI();
        }

        if (upgradePointsText != null)
        {
            UpdateUpgradePointsUI();
        }
    }

    private void UpdateExperienceUI()
    {
        if (expBar != null)
        {
            expBar.fillAmount = (float)currentExpiriance / maxExpiriance;
            expText.text = $"{currentExpiriance} / {maxExpiriance}";
        }
    }

    private void UpdateLevelUI()
    {
        if (levelText != null)
        {
            levelText.text = $"Level: {currentLevel}";
        }
    }

    private void UpdateUpgradePointsUI()
    {
        if (upgradePointsText != null)
        {
            upgradePointsDisplay.text = $"Очки: {upgradePoints}";
        }
    }

    private void OpenUpgradeMenu()
    {
        if (upgradeMenu != null)
        {
            isUpgradeMenuOpen = true;
            upgradeMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void CloseUpgradeMenu()
    {
        if (upgradeMenu != null)
        {
            isUpgradeMenuOpen = false;
            upgradeMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void SelectUpgradeSpeed()
    {
        selectedUpgrade = UpgradeType.Speed;
        if (upgradeTitle != null)
        {
            upgradeTitle.text = "Збільшити Швидкість Руху";
        }
        if (upgradeDescription != null)
        {
            upgradeDescription.text = "Increase movement speed.";
        }
        if (confirmUpgradeButton != null)
        {
            confirmUpgradeButton.interactable = upgradePoints > 0; // Доступність кнопки підтвердження вдосконалення залежить від наявності балів вдосконалення
        }
    }

    public void SelectUpgradeDamage()
    {
        selectedUpgrade = UpgradeType.Damage;
        if (upgradeTitle != null)
        {
            upgradeTitle.text = "Збільшити Шкоду";
        }
        if (upgradeDescription != null)
        {
            upgradeDescription.text = "Increase bullet damage.";
        }
        if (confirmUpgradeButton != null)
        {
            confirmUpgradeButton.interactable = upgradePoints > 0;
        }
    }

    // Вибираємо вдосконалення які наведно нижче 
    public void SelectUpgradeHealth()
    {
        selectedUpgrade = UpgradeType.Health;
        if (upgradeTitle != null)
        {
            upgradeTitle.text = "Покращти Здоров'я";
        }
        if (upgradeDescription != null)
        {
            upgradeDescription.text = "Increase maximum health.";
        }
        if (confirmUpgradeButton != null)
        {
        confirmUpgradeButton.interactable = upgradePoints > 0;
        }
    }

    public void ConfirmUpgrade()
    {
        switch (selectedUpgrade)
        {
            case UpgradeType.Speed:
                UpgradeSpeed();
                break;
            case UpgradeType.Damage:
                UpgradeDamage();
                break;
            case UpgradeType.Health:
                UpgradeHealth();
                break;
        }

        upgradePoints--; // Зменшуємо кількість доступних балів вдосконалення
        CloseUpgradeMenu();  // Закриваємо меню вдосконалення
        if (upgradePointsText != null)
        {
            UpdateUpgradePointsUI();
        }
    }

    private void UpgradeSpeed()
    {
        moveSpeed += 0.1f; // Increase speed by 1
    }

    private void UpgradeDamage()
    {
        damage += 1; // Increase damage by 1
    }

    private void UpgradeHealth()
    {
        maxHealth += healthUpgradeAmount;
        // maxHealth += 1; // Increase maximum health
    }
    
}
