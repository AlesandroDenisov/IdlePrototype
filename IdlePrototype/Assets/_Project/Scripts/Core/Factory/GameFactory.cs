using System.Collections.Generic;
using IdleArcade.Core.AssetManagement;
using IdleArcade.Services.PersistentProgress;
using IdleArcade.Data.Loot;
using IdleArcade.UI;
using IdleArcade.Services.StaticData;
using IdleArcade.Logic;
using IdleArcade.StaticData;
using IdleArcade.Enemy;
using Object = UnityEngine.Object;
using UnityEngine.AI;
using UnityEngine;
using System;
using IdleArcade.Player;
using IdleArcade.UI.Services.UIWindows;
using IdleArcade.UI.Elements;

namespace IdleArcade.Core.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        // TODO: remove PlayerGameObject, PlayerCreated from Anomaly and from here
        public GameObject PlayerGameObject { get; set; } 
        public event Action PlayerCreated;

        private readonly IAssetProvider _assets;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticData;
        private readonly IUIWindowService _uiWindowService;

        private GameObject _playerGameObject;

        public GameFactory( IAssetProvider assets
                          , IStaticDataService staticData
                          , IPersistentProgressService persistentProgressService
                          , IUIWindowService uiWindowService)
        {
            _assets = assets;
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
            _uiWindowService = uiWindowService;
        }

        public GameObject CreatePlayer(GameObject at)
        {
            _playerGameObject = InstantiateRegistered(AssetPath.PlayerPath, at.transform.position);
            PlayerCreated?.Invoke();
            PlayerGameObject = _playerGameObject;
            return _playerGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            foreach (ResourceCounter resourceCounter in hud.GetComponentsInChildren<ResourceCounter>())
            {
                resourceCounter.Construct(_persistentProgressService.Progress.WorldData, resourceCounter.ResourceType);
            }

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Init(_uiWindowService);
            }

            return hud;
        }

        public LootComponent CreateLoot(ResourceType resourceType)
        {
            string lootPath = GetAssetLootPathByResourceType(resourceType);
            if (string.IsNullOrEmpty(lootPath))
            {
                Debug.LogError("GameFactory - CreateLoot(): Invalid loot path. Unable to spawn loot.");
                return null;
            }

            LootComponent loot = InstantiateRegistered(lootPath).GetComponent<LootComponent>();
            if (loot == null)
            {
                Debug.LogError("GameFactory - CreateLoot(): Failed to get LootComponent from instantiated object.");
                return null;
            }

            loot.Construct(_persistentProgressService.Progress.WorldData);

            return loot;
        }

        private string GetAssetLootPathByResourceType(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Tokens:
                    return AssetPath.LootTokensPath;
                case ResourceType.Salvage:
                    return AssetPath.LootSalvagePath;
                case ResourceType.Glowstone:
                    return AssetPath.LootGlowstonePath;
                default:
                    return string.Empty;
            }
        }

        public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent)
        {
            if (enemyTypeId == null)
            {
                Debug.LogError($"GameFactory - CreateEnemy(): 'enemyTypeId' not found for type {enemyTypeId}");
                return null;
            }

            if (parent == null)
            {
                Debug.LogError("GameFactory - CreateEnemy(): Transform 'parent' is null");
                return null;
            }

            EnemyStaticData enemyData = _staticData.ForEnemies(enemyTypeId);
            if (enemyData == null)
            {
                Debug.LogError($"GameFactory - CreateEnemy(): Enemy data not found for type {enemyTypeId}");
                return null;
            }
            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);
            if (enemy == null)
            {
                Debug.LogError("GameFactory - CreateEnemy(): Failed to instantiate enemy prefab");
                return null;
            }

            IHealth health = enemy.GetComponent<IHealth>();
            if (health == null)
            {
                Debug.LogError("GameFactory - CreateEnemy(): Enemy prefab don't have component 'IHealth'. Add component 'EnemyHealth'");
                return null;
            }
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;

            ActorUI actorUI = enemy.GetComponent<ActorUI>();
            if (actorUI == null)
            {
                Debug.LogError("Failed to get ActorUI component from enemy");
                return null;
            }
            actorUI.Construct(health);

            NavMeshAgent navMeshAgent = enemy.GetComponent<NavMeshAgent>();
            if (navMeshAgent == null)
            {
                Debug.LogError("Failed to get NavMeshAgent component from enemy");
                return null;
            }
            navMeshAgent.speed = enemyData.MoveSpeed;

            NPCAttack attack = enemy.GetComponent<NPCAttack>();
            if (attack == null)
            {
                Debug.LogError("Failed to get NPCAttack component from enemy");
                return null;
            }
            attack.Construct(_playerGameObject.transform);
            attack.Damage = enemyData.Damage;
            attack.Cleavage = enemyData.Cleavage;
            attack.EffectiveDistance = enemyData.EffectiveDistance;

            enemy.GetComponent<NPCFollowPlayer>()?.Construct(_playerGameObject.transform);
            enemy.GetComponent<NPCRotateToPlayer>()?.Construct(_playerGameObject.transform);

            LootFromEnemySpawner lootSpawner = enemy.GetComponentInChildren<LootFromEnemySpawner>();
            lootSpawner.Construct(this);
            lootSpawner.SetLootValue(enemyData.LootValue);
            lootSpawner.SetLootType(enemyData.LootType);
            //lootSpawner.Construct(this, _randomService);
            //lootSpawner.SetLootValue(enemyData.MinLootValue, enemyData.MaxLootValue);

            return enemy;
        }

        /*        public Projectile CreateProjectile(Transform at, Transform target)
                {
                    var projectileInstance = InstantiateRegistered(AssetPath.ProjectilePath, at.transform.position);
                    if (projectileInstance == null)
                    {
                        Debug.LogError("Failed to instantiate projectile.");
                        return null;
                    }

                    Projectile projectile = projectileInstance.GetComponent<Projectile>();
                    if (projectile == null)
                    {
                        Debug.LogError("Failed to get Projectile component from instantiated object.");
                        return null;
                    }

                    projectile.Construct(at, target);

                    Debug.Log("Projectile instantiate and construct");

                    return projectile;
                }*/

        /*        public GameObject CreateAttackProjectile()
                {
                    GameObject attackProjectile = InstantiateRegistered(AssetPath.AttackProjectile);

                    if (attackProjectile == null)
                    {
                        Debug.LogError("Failed to get attackProjectile component from instantiated object.");
                        return null;
                    }

                    Debug.Log("attackProjectile instantiate");

                    return attackProjectile;
                }*/

/*        public GameObject CreateUI()
        {
            GameObject uiWindows = Object.Instantiate();

            return uiWindows;
        }*/

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        // Add a progress to a list
        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        // Instantiate a prefab at point
        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
            if (gameObject == null)
            {
                Debug.LogError($"Failed to instantiate object at path: {prefabPath}");
                return null;
            }

            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        // Instantiate a prefab
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);
            if (gameObject == null)
            {
                Debug.LogError($"Failed to instantiate object at path: {prefabPath}");
                return null;
            }

            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        // Register a player progress
        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

    }
}