using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Shield : MonoBehaviour
{
    [SerializeField] private Bullet.BulletType _bulletTypeThatCanDisableShield = Bullet.BulletType.NonDefault;
    [SerializeField] private GameObject _shieldPrefab;

    public Bullet.BulletType BulletTypeThatCanDisableShield => _bulletTypeThatCanDisableShield;

    private bool _isShieldActive;
    public bool IsShieldActive => _isShieldActive;

    private void Start()
    {
        if (SceneController.instance.GetCurrentScene() == SceneController.Scene.Level2)
        {
            _isShieldActive = true;
            _shieldPrefab.SetActive(true);
        }
    }

    public void DisableShieldIfCorrectBulletType(Bullet.BulletType bulletType)
    {
        if (bulletType == BulletTypeThatCanDisableShield)
        {
            _isShieldActive = false;
            _shieldPrefab.SetActive(false);
        }
    }
}
