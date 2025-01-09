using IdleArcade.Data;
using IdleArcade.Logic;
using IdleArcade.Services.PersistentProgress;
using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace IdleArcade.Data.Loot
{
    public class LootComponent : MonoBehaviour, ISavedProgress
    {
        public GameObject LootPrefab;
        public GameObject LootPickupFxPrefab;
        public GameObject LootPickupPopup;
        public TMP_Text LootText; //TextMeshPro LootText;

        private WorldData _worldData;
        private Loot _loot;

        private const float DelayBeforeDestroying = 2f;

        private string _id;
    
        private bool _isLootPickedUp;
        private bool _isLoadedFromProgress;

        // Reference to the NiceVibrationsManager
        private NiceVibrationsManager _vibrationsManager;

        // TODO: remove the reference to the data from here when uploading data to the server or creating multithreading
        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        } 

        public void LoadProgress(PlayerProgress progress)
        {
            _id = GetComponent<UniqueId>().Id;
      
            LootComponentData data = progress.WorldData.ResourceData.LootPiecesOnScene.Dictionary[_id];
            Initialize(data.Loot);
            transform.position = data.Position.AsUnityVector();

            _isLoadedFromProgress = true;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        } 

        private void Start()
        {
            if(!_isLoadedFromProgress)
                //TODO: Fix NullReferenceException: Object reference not set to an instance of an object
                _id = GetComponent<UniqueId>().Id;

            // Find and assign NiceVibrationsManager
            _vibrationsManager = FindObjectOfType<NiceVibrationsManager>();
            if (_vibrationsManager == null)
                Debug.LogError("NiceVibrationsManager not found in the scene!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isLootPickedUp)
            {
                _isLootPickedUp = true;
                PickupLoot();
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isLootPickedUp) return;

            LootComponentDataDictionary lootPiecesOnScene = progress.WorldData.ResourceData.LootPiecesOnScene;

            if (!lootPiecesOnScene.Dictionary.ContainsKey(_id))
                lootPiecesOnScene.Dictionary.Add(_id, new LootComponentData(transform.position.AsVectorData(), _loot));
        }

        private void PickupLoot()
        {
            UpdateWorldData();
            HideLootPrefab();
            PlayPickupFx();
            ShowText();
            Debug.Log("Loot picked up.");

            // Trigger a heavy impact using NiceVibrationsManager
            if (_vibrationsManager != null)
                _vibrationsManager.TriggerHeavyImpact();

            Destroy(gameObject, DelayBeforeDestroying);
            //StartCoroutine(StartDestroyTimer());
        }


        private void UpdateWorldData()
        {
            UpdateCollectedLootAmount();
            RemoveLootPieceFromSavedPieces();
        }

        private void UpdateCollectedLootAmount()
        {
            //Debug.Log($"Collected Loot: Value={_loot.Value}, ResourceType={_loot.ResourceType}");
            _worldData.ResourceData.Collect(_loot);
        }

        private void RemoveLootPieceFromSavedPieces()
        {
            LootComponentDataDictionary savedLootPieces = _worldData.ResourceData.LootPiecesOnScene;

            if (savedLootPieces.Dictionary.ContainsKey(_id)) 
                savedLootPieces.Dictionary.Remove(_id);
        }

        private void HideLootPrefab()
        {
            LootPrefab.SetActive(false);
            LootPickupFxPrefab.SetActive(false);
        }

        private void PlayPickupFx()
        {
            GameObject lootFx = Instantiate(LootPickupFxPrefab, transform.position, Quaternion.identity);
            lootFx.transform.parent = this.transform;
        }

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            LootPickupPopup.SetActive(true);
        }
        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(DelayBeforeDestroying);
            Destroy(gameObject);    
        }
    }
}