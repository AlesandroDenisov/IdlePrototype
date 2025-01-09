using System;
using UnityEngine;

namespace IdleArcade.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        private const string InitialPointTag = "InitialPoint";

        public PlayerHealth Health;
    
        public PlayerMove Move;
        public PlayerAttack Attack;

        public GameObject DeathFx;
        private bool _isDead;

        private void Start()
        {
            Health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && Health.Current <= 0) 
                Die();
        }

        private void Die()
        {
            _isDead = true;
            Move.enabled = false;
            Attack.enabled = false;

            transform.position = GameObject.FindWithTag(InitialPointTag).transform.position;
            // TODO: reset angles
            Health.Current = Health.Max;

            Move.enabled = true;
            Attack.enabled = true;
            _isDead = false;

            //Instantiate(DeathFx, transform.position, Quaternion.identity);
        }
    }
}