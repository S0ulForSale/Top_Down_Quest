using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Managers;
using PixelCrushers.DialogueSystem;
using PixelCrushers.QuestMachine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Animator animator;
    private bool canMove = true;
    // [SerializeField]
    // private Vector2 lastMovement;

    [Header("Dash")]
    [SerializeField]
    float dashSpeed = 10f;
    [SerializeField]
    float dashDirection = 1f;
    [SerializeField]
    float dashPoint = 2f;
    [SerializeField]
    float dashDistance = 5f;
    public int maxDashCount = 2; // Максимальна кількість Dash
    public float dashCdTm = 2.0f; // Відновлення 

    private int currentDashCount = 0; // Використані стрибки
    private bool isDashingCdTm = false;
    bool isDashing; 
    
    //________________________________End
    Vector2 movement; // Оголосити змінну movement на рівні класу 

    private HealthSystem healthSystem;
    //________________________________
    [SerializeField]
    int currentExpiriance, maxExpiriance;
    public int currentLevel;
    public Image expBar;
    public TMP_Text levelText;
    public TMP_Text expText;
    //________________________________
    private PlayerStats playerStats;
    // [SerializeField]
    // private ExpManager expi;
    public static PlayerController Instance;

    private void OnEnable()
    {
        ExpManager.Instance.OnExpChange += HandExpChange;
    }

    private void OnDisable()
    {
        ExpManager.Instance.OnExpChange -= HandExpChange;
    }

    private void HandExpChange(int newExp)
    {
        currentExpiriance += newExp;
        if(currentExpiriance >= maxExpiriance)
        {
            LevelUp();
        }
        UpdateExperienceUI();

        // // Вираховуємо відношення заповнення анімаційної стрічки 
        // float fillAmount = (float)currentExpiriance / maxExpiriance;
        
        // // Встановлюємо заповнення анімаційної стрічки 
        // expBar.fillAmount = fillAmount;
    }

    private void Start()
    {
        if(Instance == null)
        {
        rb = GetComponent<Rigidbody2D>();
        Instance = this;
        }
        healthSystem = GetComponent<HealthSystem>();
        // playerStats = GetComponent<PlayerStats>();
        playerStats = PlayerStats.Instance;

       

        UpdateExperienceUI();
        UpdateLevelUI();
        //UpdateUpgradePointsUI();
    // expi.Initialize(0,100 * currentLevel * Mathf.Pow(currentLevel, 0.5f));
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExpiriance = 0;
        maxExpiriance = (int)(0.1f * currentLevel * Mathf.Pow(currentLevel, 0.5f) * 100);

        playerStats.LevelUp();

        healthSystem.maxHealth += playerStats.maxHealth;
        healthSystem.currentHealth = healthSystem.maxHealth;

        UpdateLevelUI();
        UpdateExperienceUI();
        // healthSystem.maxHealth += 1;
        // healthSystem.currentHealth = healthSystem.maxHealth;
        //UpdateUpgradePointsUI();
    }


    void Update()
    {
        if(isDashing)
        {return;}

        // Оновлення тексту для відображення поточного рівня 
        levelText.text = currentLevel.ToString();

        // Оновлення тексту для відображення кількості до наступного рівня 
        int remainingExp = maxExpiriance - currentExpiriance;
        expText.text = currentExpiriance.ToString() + " / " + maxExpiriance.ToString();

        InputManager.instance.dash.AddListener(TriggerDash);

        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartCoroutine(Dash(movement)); // Передача вектору руху у метод Dash() 
        // }
    }

    private void FixedUpdate()
    {
        
        if(isDashing)
        {return;}
        // // Отримати вхід користувача
        // float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");


        
        // movement = new Vector2(moveHorizontal, moveVertical);

        // if (movement != Vector2.zero)
        // {
        //     lastMovement = movement; // Збереження останнього напрямку руху 
        // }

        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);
        

        // // Нормалізувати вектор швидкості, щоб забезпечити однакову швидкість у всіх напрямках 
        // movement = movement.normalized * (moveSpeed + playerStats.moveSpeed);

        // // Оновити позицію персонажа залежно від швидкості руху і часу 
        // rb.MovePosition(Joystick.Horizontal + movement * ime.deltaTime);
        // rb.MovePosition(rb.position + movement * Time.deltaTime);
        // Отримайте вхід користувача з джойстика
        // float moveHorizontal = _joystick.Horizontal;
        // float moveVertical = _joystick.Vertical;

        Vector2 movement = InputManager.instance.axis;
        // Нормалізувати вектор руху, якщо його довжина більша за 1
        if (movement.sqrMagnitude > 1f)
        {
            movement.Normalize();
        }

        // Установка параметрів для анімації
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if(canMove)
        {

        // Нормалізувати вектор швидкості, щоб забезпечити однакову швидкість у всіх напрямках 
        movement = movement.normalized * (moveSpeed + playerStats.moveSpeed);

        // Оновити позицію персонажа залежно від швидкості руху і часу 
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }

        if (DialogueManager.IsConversationActive)
        {
            // Якщо ведеться діалог, вимкниця рух гравця
            canMove = false;
        }
        else
        {
            // Якщо діалог не ведеться, включить рух гравця
            canMove = true;
        }
    }
    public static GameObject[] FindObjectsOfTypeWithTag(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        return taggedObjects;
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int GetMaxDashCount()
    {
        return maxDashCount; // Повертаємо кількість максимальних Dash з відповідної змінної
    }

    public int GetCurrentDashCount()
    {
        return currentDashCount; // Повертаємо кількість максимальних Dash з відповідної змінної
    }
    private void TriggerDash()
    {
        if(canMove)
        if (!isDashing)
        {
            StartCoroutine(Dash(InputManager.instance.nonZeroAxis));
        }
    }
    private IEnumerator Dash(Vector2 dashDirection) // Додайте параметр dashDirection  dashSpace
    {

        if(!isDashingCdTm && currentDashCount < maxDashCount)
        {
            isDashing = true;
            animator.SetTrigger("DashTrigger");

            // Вимкнути колізію гравця з ворогами 
            Collider2D playerCollider = GetComponent<Collider2D>();
            Collider2D[] enemyColliders = FindObjectsOfTypeWithTag("Enemy").Select(enemy => enemy.GetComponent<Collider2D>()).ToArray();
            foreach (Collider2D enemyCollider in enemyColliders)
            {
                Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);
            }

            // Зберегти напрямок дашу для відтворення анімації
            float dashHorizontal = dashDirection.x;
            float dashVertical = dashDirection.y;

            Vector2 startPosition = rb.position;
            Vector2 endPosition = startPosition + dashDirection.normalized * dashDistance;

            float dashTime = dashDistance / dashSpeed;
            float elapsedTime = 0f;

            while (elapsedTime < dashTime)
            {
                rb.MovePosition(Vector2.Lerp(startPosition, endPosition, elapsedTime / dashTime));
                elapsedTime += Time.deltaTime;

                // Оновити параметри анімації
                animator.SetFloat("DashHorizontal", dashHorizontal);
                animator.SetFloat("DashVertical", dashVertical);
                animator.SetFloat("DashSpeed", dashDirection.sqrMagnitude); // Встановити швидкість 1, щоб програти анімацію дашу dashDirection.sqrMagnitude
            

                yield return null;
            }

            rb.MovePosition(endPosition);

            // Увімкнути колізію гравця з ворогами 
            foreach (Collider2D enemyCollider in enemyColliders)
            {
                Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
            }

            // Затримка після дашу
            yield return new WaitForSeconds(dashPoint);

            // Зберігаємо нульові значення для анімації, щоб персонаж не програв анімацію після дашу
            // animator.SetFloat("DashHorizontal", 0f);
            // animator.SetFloat("DashVertical", 0f);
            // animator.SetFloat("DashSpeed", 0f);
            isDashing = false;

            StartCoroutine(DashCd());
        }
    }

    private IEnumerator DashCd()
    {
        isDashingCdTm = true; // перезарядка / відновлення (хз);

        yield return new WaitForSeconds(dashCdTm);

        currentDashCount = 0; // збільшуємо використані стрибки

        isDashingCdTm = false; 
    }


    // public int GetUpgradePoints()
    // {
    //     return upgradePoints;damage
    // }

    private void UpdateExperienceUI()
    {
        expBar.fillAmount = (float)currentExpiriance / maxExpiriance;
        expText.text = $"{currentExpiriance} / {maxExpiriance}";
    }

    private void UpdateLevelUI()
    {
        levelText.text = $"{currentLevel}";
    }
    
}
