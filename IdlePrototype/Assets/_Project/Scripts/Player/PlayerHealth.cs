using System;
using IdleArcade.Data;
using IdleArcade.Services.PersistentProgress;
using IdleArcade.Logic;
using UnityEngine;

namespace IdleArcade.Player
{
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {    
        private Stats _stats;

        public event Action HealthChanged;

        public float Current
        {
            get => _stats.CurrentHP;
            set
            {
                if (value != _stats.CurrentHP)
                {
                    _stats.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _stats.MaxHP;
            set => _stats.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.PlayerStats;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerStats.CurrentHP = Current;
            progress.PlayerStats.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;
      
            Current -= damage;
            Debug.Log($"-{damage} HP. Player HP=${Current}");
        }
    }
}