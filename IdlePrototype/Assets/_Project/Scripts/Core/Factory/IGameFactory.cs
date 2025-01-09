using IdleArcade.Services;
using IdleArcade.Services.PersistentProgress;
using IdleArcade.Data.Loot;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleArcade.Core.Factory
{
    public interface IGameFactory: IService
    {
        GameObject CreatePlayer(GameObject at);
        GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
        LootComponent CreateLoot(ResourceType resourceType);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject PlayerGameObject { get; } // TODO: remove PlayerGameObject, PlayerCreated from Anomaly and from here
        event Action PlayerCreated;
        //Projectile CreateProjectile(Transform at, Transform target);
        void Cleanup();
        void Register(ISavedProgressReader savedProgress);
    }
}