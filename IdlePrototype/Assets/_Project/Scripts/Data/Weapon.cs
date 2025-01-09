using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleArcade.Data
{
    [Serializable]
    public class Weapon
    {
/*        public enum WeaponType
        {
            Hands = 0,
            Pistol = 1,
            Uzi = 2,
            Shotgun = 3
        }*/

        [Header("Weapon settings")] public string WeaponName = "Base Gun weapon";
        public WeaponTypeId WeaponClass = WeaponTypeId.Gun;

        //public GameObject WeaponPrefab;
        //public Sprite WeaponImage;
        public GameObject MuzzleEffect;
        
        //[Tooltip("Number of bullet that can load this weapon.")]
        //public int WeaponMagazine;

        [Tooltip("Time to reload the weapon")] public float ReloadTime;

        [Tooltip("Time between bullets spawn.")]
        public float FireRate;

        [Tooltip("Id for the type of bullet use for this weapon.")]
        public int BulletId;

        [Tooltip("Bullets prefab to spawn.")] public GameObject BulletPrefab;
        [Tooltip("Bullet damage.")] public float Damage;
        [Tooltip("Launched bullet speed.")] public float BulletSpeed;
        public bool hasShells;
        public GameObject ShellPrefab;

        //[Header("Weapon drop prefab")]
        //[Tooltip("When the player drop the weapon this object appear in the floor, and is use to get the weapon again.")]
        //public GameObject WeaponDrop;
    }
}
