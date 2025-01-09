using IdleArcade.Core.Factory;
using IdleArcade.Data;
using IdleArcade.Logic;
using IdleArcade.Services;
using IdleArcade.Services.PersistentProgress;
using UnityEditor;
using UnityEngine;

namespace IdleArcade.Enemy
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public EnemyTypeId EnemyTypeId;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        public bool IsKilled;
        private string _id;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        private void OnDestroy()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Killed;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
            {
                //IsKilled = false;
                progress.KillData.ClearedSpawners.Remove(_id);
            }
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (IsKilled)
                progress.KillData.ClearedSpawners.Add(_id);
                // TODO: create serializable class dictionary for count kills
        }

        private void Spawn()
        {
            Debug.Log("EnemySpawner - Run Spawn()");

            GameObject enemy = _factory.CreateEnemy(EnemyTypeId, transform);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Killed;
        }

        private void Killed()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Killed;

            IsKilled = true;
        }
    }
}