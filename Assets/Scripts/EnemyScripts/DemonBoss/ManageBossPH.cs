using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBossPH : MonoBehaviour
{
    [SerializeField] private Shield _shield;

    public GameObject portalPrefab;
    public event Action OnDemonBossDeath;

    public Animator demoniAnim;
    public int currentHealth;
    public int maxHealth;
    public int baseExpAmount = 10;
    public int expIncreasePerLevel = 5;

    // Start is called before the first frame update
    void Start()
    {
        demoniAnim = GetComponent<Animator>();
        //playerStats = PlayerStats.Instance;
    }

    public void HitEnemy(int damageToGive, Bullet.BulletType bulletType)
    {
        if (_shield.IsShieldActive)
        {
            _shield.DisableShieldIfCorrectBulletType(bulletType);
            DealDamage(damageToGive);
        }
        else
        {
            DealDamage(damageToGive * 10);
        }
    }

    private void DealDamage(int damageToGive)
    {
        currentHealth -= damageToGive;
        if (currentHealth <= 0)
        {
            int expAmount = baseExpAmount + (expIncreasePerLevel * PlayerStats.Instance.currentLevel);
            ExpManager.Instance.AddExp(expAmount);

            // Створюємо портал
            GameObject portal = Instantiate(portalPrefab, transform.position, Quaternion.identity);

            OnDemonBossDeath?.Invoke();

            // Знищуємо боса
            Destroy(gameObject);
        }
    }
}
