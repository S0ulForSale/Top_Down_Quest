using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private List<WeaponTemplate> weapons;

    public WeaponTemplate GetWeapon(string id)
    {
        return weapons.First(w => w.id == id);
    }
}