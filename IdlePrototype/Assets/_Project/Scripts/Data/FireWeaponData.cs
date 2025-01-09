using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleArcade.Data
{
    public class FireWeaponData : MonoBehaviour
    {
        public List<Weapon> Weapons;

        public Weapon GetWeaponByName(string weaponName)
        {
            return Weapons.Find(e => e.WeaponName == weaponName);
        }
    }
}

