using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class WeaponTemplate
{
    public string id;
    public ProjectileTemplate projectileData;
    public float autoAimRadius = 5.0f;
    public int bulletsNum = 1;
    public float bulletSpread = 0.1f;
    public float singleBulletSpread = 0.05f;
    public int maxAmmo = 10;
    public float reloadTime = 2f;
    public float recoilTime = 0.1f;
}

[Serializable]
public class ProjectileTemplate
{
    public Bullet bulletPref;
    public float destroysShield;
    public float bulletSpeed = 20;
    public float timeToLive = 10;
    public float bulletSize = 1;
    public float bulletDamage = 1;
    public float bulletPenetration = 1;

    public Bullet GetBullet(Quaternion angle, Vector3 pos, float additionalDamage)
    {
        Bullet bullet = Object.Instantiate(bulletPref, pos, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        var bulletDir = angle * Vector3.up;
        rb.AddForce(bulletDir * bulletSpeed, ForceMode2D.Impulse);
        bullet.transform.rotation = bulletPref.transform.rotation * angle;
        var damage = bulletDamage + additionalDamage;
        var fraction = damage % 1;
        bullet.damageToGive = (int)(damage - fraction + 0.01f) + (Random.value < fraction ? 1 : 0);
        bullet.timeToLive = timeToLive;
        //   bullet.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        return bullet;
    }
}