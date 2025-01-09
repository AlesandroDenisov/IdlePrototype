using System;
using IdleArcade.Logic;
using UnityEngine;

namespace IdleArcade.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField]
        private float _current;

        [SerializeField]
        private float _max;

        public event Action HealthChanged;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            Debug.Log($"Enemy take damage {damage}. {Current} HP left. ");
            HealthChanged?.Invoke();
        }
    }
}