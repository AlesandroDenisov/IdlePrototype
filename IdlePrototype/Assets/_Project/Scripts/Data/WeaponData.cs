using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IdleArcade.Data
{
    [Serializable]
    public class WeaponData
    {
        //public string name;
        //public Rigidbody bombPrefab;
        //public float min, max;

        //public List<WeaponsDictionary> Weapons = new List<WeaponsDictionary>();
        public int Damage;
        public WeaponTypeId WeaponType;

        public WeaponData()
        {
/*            foreach (WeaponTypeId weaponType in Enum.GetValues(typeof(WeaponTypeId)))
            {
                Weapons.Add(new WeaponsDictionary(WeaponTypeId, 0));
            }*/
            Damage = 1;
            WeaponType = WeaponTypeId.Gun;
        }

        public int GetDamage()
        {
            return Damage;
        }

        public WeaponTypeId GetWeaponType() 
        {
            return WeaponType;
        }
    }
}