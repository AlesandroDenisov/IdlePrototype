using System;
using System.Linq;
using IdleArcade.Player;
using IdleArcade.Core.Factory;
using IdleArcade.Services;
using IdleArcade.Logic;
using UnityEngine;

namespace IdleArcade.Enemy
{
    public class AnomalyAttack : Attack
    {
        public float AttackCooldown = 3.0f;
        public float AnomalyArea = 4f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10;

        private IGameFactory _factory;
        private Transform _playerTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private Collider[] _hits = new Collider[1];
        private int _layerMAsk;
        private bool _attackIsActive;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();

            _layerMAsk = 1 << LayerMask.NameToLayer("Player");
      
            _factory.PlayerCreated += OnPlayerCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if(CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
            //PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1.0f);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);

                _attackCooldown = AttackCooldown;
                _isAttacking = false;
            }
        }

        public new void DisableAttack()
        {
            _attackIsActive = false;
        }

        public new void EnableAttack()
        {
            _attackIsActive = true;
        }

        private void OnPlayerCreated()
        {
            _playerTransform = _factory.PlayerGameObject.transform;
        }

        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            _attackCooldown -= Time.deltaTime;
        }

        private bool Hit(out Collider hit)
        {
            var hitAmount = Physics.OverlapSphereNonAlloc(StartPoint(), AnomalyArea, _hits, _layerMAsk);

            hit = _hits.FirstOrDefault();
      
            return hitAmount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                    transform.forward * EffectiveDistance;
        }

        private bool CanAttack()
        {
            return _attackIsActive && !_isAttacking && CooldownIsUp();
        }

        private void StartAttack()
        {
            transform.LookAt(_playerTransform);
            _isAttacking = true;
        }
    }
}