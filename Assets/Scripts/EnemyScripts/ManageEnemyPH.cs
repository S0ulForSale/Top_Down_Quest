using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEnemyPH : MonoBehaviour
{
    public Animator zombiAnim;
    public int currentHealth;
    public int maxHealth;
    public int baseExpAmount = 10;
    public int expIncreasePerLevel = 5;

    // Start is called before the first frame update
    void Start()
    {
        zombiAnim = GetComponent<Animator>();
        //playerStats = PlayerStats.Instance;
    }

    // Update is called once per frame 
    void Update()
    {
        
    }
    public void HitEnemy(int damageToGive)
    {
        currentHealth -= damageToGive;
        if(currentHealth <= 0)
        {
            int expAmount = baseExpAmount + (expIncreasePerLevel * PlayerStats.Instance.currentLevel);
            ExpManager.Instance.AddExp(expAmount);
            zombiAnim.SetBool("IsDead", true);
            Destroy(gameObject);
                
        }
    }

}
