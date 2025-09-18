using System.Collections;
using System.Collections.Generic;
using InventoryStuff;
using InventoryStuff.Model;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shooting : MonoBehaviour
{
    //public Transform firePoint;
    //public Button shootZone;
    public GameObject bulletPref;
    private Weapon weapon = new Weapon();
    [SerializeField] private WeaponHolder weaponHolder;
    public Animator animator;
    public AudioClip shotSound;
    private AudioSource audio;


    public TMP_Text ammoText;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Встановлюємо початкову кількість патронів

        Inventory.instance.OnEquip.AddListener((item) => weapon.UpdateWeapon(weaponHolder.GetWeapon(item.equipableID)));
        weapon.UpdateWeapon(weaponHolder.GetWeapon("gun"));
        UpdateAmmoText();
        // Додаємо обробник події натискання кнопки стрільби
        InputManager.instance.shoot.AddListener(ShootTrigger);
        //ShootTrigger();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame 
    void Update()
    {
        weapon.Update(Time.deltaTime);
        if (InputManager.instance.firing)
            ShootTrigger();
        UpdateAmmoText();
    }

    private void ShootTrigger()
    {
        ShootToClosestEnemy();
    }

    void ShootToClosestEnemy()
    {
        if (!weapon.canShoot)
            return;

        GameObject closestEnemy = FindClosestEnemyWithTag("Enemy", "DemonBoss");
        animator.SetTrigger("ShootTrigger");

        Vector3 direction = (closestEnemy == null)
            ? InputManager.instance.nonZeroAxis.normalized
            : (closestEnemy.transform.position - transform.position).normalized;

        var perpendicular = Vector3.Cross(Vector3.forward, direction);
        var sideAngle = Quaternion.LookRotation(Vector3.forward, perpendicular);
        var forwardAngle = Quaternion.LookRotation(Vector3.forward, direction);

        if (!weapon.TryShoot()) Debug.LogError("Should Be shooting :_( ");
        for (int i = -weapon.data.bulletsNum / 2; i <= weapon.data.bulletsNum / 2; i++)
        {
            var arcPosition = weapon.data.bulletSpread * i + weapon.data.singleBulletSpread * (Random.value - 0.5f);
            var bulletAngle = Quaternion.LerpUnclamped(forwardAngle, sideAngle, arcPosition);
            var bullet =
                weapon.data.projectileData.GetBullet(bulletAngle, transform.position,
                    ((float)PlayerStats.Instance.damage) / weapon.data.bulletsNum);
            Collider2D playerCollider = GetComponent<Collider2D>();
            Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(playerCollider, bulletCollider);
            audio.PlayOneShot(shotSound);

            CameraShake.Instance.ShakeCamera(0.2f, .1f);
        }


        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", 0f);
        animator.SetFloat("ShootDirection", 0f);

        StartCoroutine(ResetAnimation());
    }

    // void Shoot()
    // {
    //     if (isReloading)
    //         return;
    //
    //     if (currentAmmo > 0)
    //     {
    //         GameObject closestEnemy = FindClosestEnemyWithTag("Enemy"); // Метод для пошуку найближчого ворога за тегом
    //         if (closestEnemy != null)
    //         {
    //             Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
    //
    //             GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);
    //             Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    //             rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
    //             bullet.transform.rotation =
    //                 Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    //
    //             Collider2D playerCollider = GetComponent<Collider2D>();
    //             Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
    //             Physics2D.IgnoreCollision(playerCollider, bulletCollider);
    //
    //             // Активуємо параметр аніматора "Shoot" та встановлюємо напрямок анімації
    //             // animator.SetBool("Shoot", true);
    //             // animator.SetFloat("ShootDirection", Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    //             animator.SetFloat("Horizontal", 0f);
    //             animator.SetFloat("Vertical", 0f);
    //             animator.SetFloat("ShootDirection", 0f);
    //
    //             currentAmmo--;
    //             StartCoroutine(ResetAnimation());
    //         }
    //     }
    // }

    // animator.SetFloat("Horizontal", 0f);
    // animator.SetFloat("Vertical", 0f);
    // animator.SetFloat("ShootDirection", 0f);

    GameObject FindClosestEnemyWithTag(params string[] tags)
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (string tag in tags)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject enemy in enemies)
            {
                Vector3 directionToEnemy = enemy.transform.position - currentPosition;
                float distanceToEnemy = directionToEnemy.sqrMagnitude;
                var aimR = weapon.data.autoAimRadius;
                if (distanceToEnemy < closestDistance && distanceToEnemy < aimR * aimR)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }


    IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(1f); // Затримка для анімації
        animator.SetBool("ShootToClosestEnemy", false);
    }

    void UpdateAmmoText()
    {
        ammoText.text = weapon.currentAmmo.ToString() + "/" + weapon.data.maxAmmo.ToString();
    }
}