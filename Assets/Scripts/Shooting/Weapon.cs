using System;
using Unity.VisualScripting;
using UnityEngine;

namespace InventoryStuff
{
    [Serializable]
    public class Weapon
    {
        public int currentAmmo { get; private set; } = 1;
        public float reloadTimeLeft{ get; private set; }
        public float recoilTimeLeft{ get; private set; }
        public bool isReloading => reloadTimeLeft > 0;
        public bool isRecoil => recoilTimeLeft > 0;
        public bool isEmpty => currentAmmo == 0;
        public bool canShoot => !isReloading && !isRecoil && !isEmpty;


        [SerializeField] private WeaponTemplate _template;
        public WeaponTemplate data => _template;
        public void UpdateWeapon(WeaponTemplate template)
        {
            _template = template;
            reloadTimeLeft = 0;
            recoilTimeLeft = 0;
            currentAmmo = template.maxAmmo;
        }

        public void Update(float deltaTime)
        {
            if (reloadTimeLeft > 0 && reloadTimeLeft < deltaTime)
                currentAmmo = _template.maxAmmo;
            reloadTimeLeft -= deltaTime;
            recoilTimeLeft -= deltaTime;
        }

        public bool TryShoot()
        {
            
            if (!canShoot) return false;
            currentAmmo--;
            recoilTimeLeft = _template.recoilTime;
            if (isEmpty) reloadTimeLeft = _template.reloadTime;
            return true;
        }
    }
}