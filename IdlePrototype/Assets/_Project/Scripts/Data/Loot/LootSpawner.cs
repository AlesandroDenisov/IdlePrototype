using IdleArcade.Core.Factory;
using IdleArcade.Logic;
using UnityEngine;
using System;
using Unity.Mathematics;
using IdleArcade.Core.AssetManagement;

namespace IdleArcade.Data.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        public BuildingProduce BuildingProduce;
    
        private IGameFactory _factory;

        [SerializeField] private int _value;
        [SerializeField] private ResourceType _resourceType;
        private int _minValue;
        private int _maxValue;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        private void Awake()
        {
            BuildingProduce = GetComponent<BuildingProduce>();
        }

        private void Start()
        {
            BuildingProduce.Happened += SpawnLoot;
        }

        private void OnDestroy()
        {
            BuildingProduce.Happened -= SpawnLoot;
        }

        public void SetLootValue(int value)
        {
            _value = value;
        }

        public void SetLootValue(int min, int max)
        {
            _minValue = min;
            _maxValue = max;
        }

        public void SetLootType(ResourceType resourceType)
        {
            _resourceType = resourceType;
        }

        private void SpawnLoot()
        {
            if (_factory == null)
            {
                Debug.LogError("Factory is not set. Unable to spawn loot.");
                return;
            }

            BuildingProduce.Happened -= SpawnLoot;

            LootComponent lootComponent = _factory.CreateLoot(_resourceType);
            if (lootComponent == null)
            {
                Debug.LogError("Failed to create loot. Factory returned null.");
                return;
            }

            lootComponent.transform.position = transform.position;
            lootComponent.GetComponent<UniqueId>().GenerateId();

            Loot lootItem = GenerateLoot(); // fill lootItem with values
            lootComponent.Initialize(lootItem); // init lootItem on a scene -
        }



        private Loot GenerateLoot()
        {
            Loot loot = new Loot(_value, _resourceType);
            //Debug.Log($"Loot has been generate: Value={_value}, ResourceType={_resourceType}");
            return loot;
        }
    }
}