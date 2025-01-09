using System;
using System.Collections;
using UnityEngine;

namespace IdleArcade.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour, ILootableEvent
    {
        public EnemyHealth Health;

        public GameObject DeathFx;

        public event Action Happened;

        private void Start()
        {
            Health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Debug.Log("Enemy was killed");

            Health.HealthChanged -= OnHealthChanged;
      
            SpawnDeathFx();

            StartCoroutine(DestroyTimer());
      
            Happened?.Invoke();
        }

        private void SpawnDeathFx()
        {
            Instantiate(DeathFx, transform.position, Quaternion.identity);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}