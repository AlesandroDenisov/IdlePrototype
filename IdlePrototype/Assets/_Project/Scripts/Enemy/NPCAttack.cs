using IdleArcade.Data;
using IdleArcade.Logic;
using System.Collections;
using UnityEngine;

namespace IdleArcade.Player
{
    public class NPCAttack : Attack //, ISavedProgressReader
    {
        public float AttackCooldown = 2f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10;

        [Header("Weapon rotation speed:")]
        public float RotationSpeed;

        [Header("Weapons Database:")]
        public FireWeaponData WeaponData;
        public int CurrentWeaponIndex = 0;

        [Header("Position to instantiate bullets")]
        public Transform[] BulletPoints;

        public GameObject spawnLocatorMuzzleFlare;

        [SerializeField] private GameObject _weapon;
        private Transform _playerTransform;
        private int _layerMask;
        private bool _isAttackActive;
        private float _currentAttackCooldown;
        private Vector3 _positionToLook;
        private Stats _stats;
        private Coroutine _attackCoroutine;

        public void Construct(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (_isAttackActive)
            {
                UpdateCooldown();

                if (IsPlayerInRange())
                {

                    if (CanAttack())
                    {
                        if (_attackCoroutine == null)
                        {
                            _attackCoroutine = StartCoroutine(AttackRoutine());
                        }
                    }
                }
            }
        }

        private bool CanAttack()
        {
            return _isAttackActive && _playerTransform != null && CooldownIsUp();
        }

        private bool CooldownIsUp()
        {
            return _currentAttackCooldown <= 0f;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _currentAttackCooldown -= Time.deltaTime;
            }
        }

        public new void DisableAttack()
        {
            Debug.Log("NPCAttack - DisableAttack()");
            _isAttackActive = false;

            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }

        public void EnableAttack(Transform enemy)
        {
            _playerTransform = enemy;
            _isAttackActive = true;
            Debug.Log($"NPCAttack - EnableAttack(): _currentEnemy = {_playerTransform}");
        }

        private void StartAttack()
        {
            Debug.Log("StartAttack()");
            if (IsPlayerInRange())
            {
                Shoot();
                _currentAttackCooldown = AttackCooldown;
            }
        }

        private IEnumerator AttackRoutine()
        {
            while (_isAttackActive)
            {
                if (CanAttack())
                {
                    StartAttack();
                }
                yield return new WaitForSeconds(AttackCooldown);
            }
            _attackCoroutine = null;
        }

        private bool IsPlayerInRange()
        {
            return _playerTransform != null;
        }

        private void Shoot()
        {
            Debug.Log("NPCAttack - Start Shoot()");

            if (WeaponData != null && WeaponData.Weapons != null && CurrentWeaponIndex < WeaponData.Weapons.Count)
            {
                if (WeaponData.Weapons[CurrentWeaponIndex].MuzzleEffect != null)
                {
                    Instantiate(WeaponData.Weapons[CurrentWeaponIndex].MuzzleEffect, spawnLocatorMuzzleFlare.transform.position, spawnLocatorMuzzleFlare.transform.rotation);
                }

                for (int i = 0; i < BulletPoints.Length; i++)
                {
                    var bulletPrefab = WeaponData.Weapons[CurrentWeaponIndex].BulletPrefab;
                    if (bulletPrefab != null)
                    {
                        var bullet = Instantiate(bulletPrefab, BulletPoints[i].position, BulletPoints[i].rotation);
                        bullet.GetComponent<Rigidbody>().AddForce(BulletPoints[i].forward * WeaponData.Weapons[CurrentWeaponIndex].BulletSpeed);

                        var damageComponent = bullet.GetComponent<Damage>();
                        if (damageComponent != null)
                        {
                            damageComponent.DamagePower = WeaponData.Weapons[CurrentWeaponIndex].Damage;
                        }

                        if (IsPlayerInRange())
                        {
                            _playerTransform.GetComponent<IHealth>().TakeDamage(damageComponent.DamagePower);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("NPCAttack - Shoot(): BulletPrefab is null");
                    }
                }
            }
            else
            {
                Debug.LogWarning("NPCAttack - Shoot(): WeaponData, Weapons array or MuzzleEffect is null or CurrentWeaponIndex is out of range");
            }
        }
    }
}
