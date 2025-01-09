using IdleArcade.Data;
using IdleArcade.Logic;
using IdleArcade.Services.PersistentProgress;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

namespace IdleArcade.Player
{
    public class PlayerAttack : Attack, ISavedProgressReader
    {
        public float AttackCooldown = 2f;
        
        [Header("Weapon rotation speed:")]
        public float RotationSpeed;

        [Header("Weapons Database:")]
        public FireWeaponData WeaponData;
        public int CurrentWeaponIndex = 0;

        [Header("Position to instantiate bullets")]
        public Transform[] BulletPoints;

        public GameObject spawnLocatorMuzzleFlare;

        [SerializeField] private GameObject _weapon;
        private Transform _currentEnemy;
        private bool _isAttackActive;
        private float _currentAttackCooldown;
        private Vector3 _positionToLook;
        private Stats _stats;

        private void Update()
        {
            if (_isAttackActive)
            {
                UpdateCooldown();

                if (IsEnemyInRange())
                { 
                    AimToEnemy();

                    if (CanAttack())
                    {
                        StartAttack();
                    }

/*                   if (_currentEnemy != null && CooldownIsUp())
                    {
                        StartAttack();
                    }*/
                }
            }
        }

        private bool CanAttack()
        {
            return _isAttackActive && _currentEnemy != null && CooldownIsUp();
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
            Debug.Log("PlayerAttack - DisableAttack()");
            _isAttackActive = false;
            _currentEnemy = null;
        }

        public void EnableAttack(Transform enemy)
        {
            _currentEnemy = enemy;
            _isAttackActive = true;
            Debug.Log($"PlayerAttack - EnableAttack(): _currentEnemy = {_currentEnemy}");
        }

        private void StartAttack()
        {
            if (IsEnemyInRange())
            {
                Shoot();

                float damage = GetDamage();

                if (_currentEnemy != null)
                {
                    IHealth health = _currentEnemy.GetComponent<IHealth>();
                    if (health != null)
                    {
                        health.TakeDamage(damage);
                    }
                    else
                    {
                        //Debug.LogWarning($"PlayerAttack - StartAttack(): {_currentEnemy.name} does not have an IHealth component.");
                        // Debug output to list all components on the current enemy
                        Component[] components = _currentEnemy.GetComponents<Component>();
                        foreach (var component in components)
                        {
                            Debug.Log($"Component: {component.GetType().Name}");
                        }
                    }
                }

                _currentAttackCooldown = AttackCooldown;
            }
        }

        private float GetDamage()
        {
            if (WeaponData != null && WeaponData.Weapons != null && CurrentWeaponIndex < WeaponData.Weapons.Count)
            {
                return WeaponData.Weapons[CurrentWeaponIndex].Damage;
            }
            else
            {
                Debug.LogWarning("PlayerAttack - GetDamage(): WeaponData, Weapons array or CurrentWeaponIndex is out of range.");
                return 0f;
            }
        }

        private void AimToEnemy()
        {
            //Debug.Log("PlayerAttack - Aim To Enemy()");
            UpdatePositionToLookAt();
            _weapon.transform.rotation = SmoothedRotation(_weapon.transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            if (_currentEnemy != null)
            {
                Vector3 positionDelta = _currentEnemy.position - _weapon.transform.position;
                _positionToLook = new Vector3(positionDelta.x, 0, positionDelta.z);//_weapon.transform.position.y, positionDelta.z);
            }
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private Quaternion TargetRotation(Vector3 position)
        {
            return Quaternion.LookRotation(position);
        }

        private float SpeedFactor()
        {
            return RotationSpeed * Time.deltaTime;
        }

        private bool IsEnemyInRange()
        {
            return _currentEnemy != null;
        }

        private void Shoot( )
        {
            Debug.Log("PlayerAttack - Start Shoot()");


            if (WeaponData != null && WeaponData.Weapons != null && CurrentWeaponIndex < WeaponData.Weapons.Count)
            {
                //AudioManager.instance.gunSound[_currentWeaponIndex].Play();

                if (WeaponData.Weapons[CurrentWeaponIndex].MuzzleEffect != null)
                {
                    //Instantiate(WeaponData.Weapons[CurrentWeaponIndex].MuzzleEffect, BulletPoints[0].position, BulletPoints[0].rotation);
                    Instantiate(WeaponData.Weapons[CurrentWeaponIndex].MuzzleEffect, spawnLocatorMuzzleFlare.transform.position, spawnLocatorMuzzleFlare.transform.rotation);
                }

                /*            if (WeaponData.Weapons[CurrentWeaponIndex].hasShells)
                            {
                                Instantiate(WeaponData.Weapons[CurrentWeaponIndex].ShellPrefab, shellLocator.position, shellLocator.rotation);
                            }
                            recoilAnimator.SetTrigger("recoil_trigger");*/


                for (int i = 0; i < 1; i++)
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

                        AttackCooldown = WeaponData.Weapons[CurrentWeaponIndex].FireRate;

                        //if (IsEnemyInRange())
                        //    currentEnemy.GetComponent<IHealth>().TakeDamage(damageComponent.DamagePower);
                    }
                    else
                    {
                        Debug.LogWarning("PlayerAttack - Shoot(): BulletPrefab is null");
                    }
                }
            }
            else
            {
                Debug.LogWarning("PlayerAttack - Shoot(): WeaponData, Weapons array or MuzzleEffect is null or CurrentWeaponIndex is out of range");
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.PlayerStats;
        }
    }
}