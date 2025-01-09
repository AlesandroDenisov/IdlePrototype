using IdleArcade.Data;
using IdleArcade.Services;
using IdleArcade.Services.PersistentProgress;
using IdleArcade.Services.SaveLoad;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace IdleArcade.UI.Windows
{
    public class GarageWindow : UIWindowBase, ISavedProgress
    {
        public TextMeshProUGUI SpeedText;
        public TextMeshProUGUI HPText;
        public TextMeshProUGUI TrunkText;
        public TextMeshProUGUI GunText;
        public TextMeshProUGUI DamageText;

        public Button UpgradeSpeedButton;
        public Button UpgradeHPButton;
        public Button UpgradeTrunkButton;
        public Button UpgradeGunButton;
        public Button UpgradeDamageButton;

        private ISaveLoadService _saveLoadService;

        protected override void Init()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            RefreshPlayerStatsText();
            SetupButtonListeners();
        }

        private void SetupButtonListeners()
        {
            UpgradeSpeedButton.onClick.AddListener(OnUpgradeSpeedButtonClicked);
            UpgradeHPButton.onClick.AddListener(OnUpgradeHPButtonClicked);
            UpgradeTrunkButton.onClick.AddListener(OnUpgradeTrunkButtonClicked);
            UpgradeGunButton.onClick.AddListener(OnUpgradeGunButtonClicked);
            UpgradeDamageButton.onClick.AddListener(OnUpgradeDamageButtonClicked);
        }

        protected override void SubscribeUpdates()
        {
            Progress.PlayerStats.Changed += OnStatsChanged;
        }

        private void OnStatsChanged()
        {
            RefreshPlayerStatsText();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.PlayerStats.Changed -= OnStatsChanged;

            UpgradeSpeedButton.onClick.RemoveListener(OnUpgradeSpeedButtonClicked);
            UpgradeHPButton.onClick.RemoveListener(OnUpgradeHPButtonClicked);
            UpgradeTrunkButton.onClick.RemoveListener(OnUpgradeTrunkButtonClicked);
            UpgradeGunButton.onClick.RemoveListener(OnUpgradeGunButtonClicked);
            UpgradeDamageButton.onClick.RemoveListener(OnUpgradeDamageButtonClicked);
        }

        private void RefreshPlayerStatsText()
        {
            SpeedText.text = Progress.PlayerStats.Speed.ToString();
            HPText.text = Progress.PlayerStats.MaxHP.ToString();
            TrunkText.text = Progress.PlayerStats.MaxTrunk.ToString();
            GunText.text = Progress.PlayerStats.Gun.ToString();
            DamageText.text = Progress.PlayerStats.Damage.ToString();
        }


        public void PanelActiveOrDeactive(bool isActive)
        {
            if (isActive) 
                StartPanel();
            else 
                ClosePanel();
            //FindAnyObjectByType<AudioEffects>().AudioMenuActive();
        }

        private void StartPanel()
        {
            gameObject.SetActive(true);
        }

        private void ClosePanel()
        {
            gameObject.SetActive(false);
        }

        private void OnUpgradeSpeedButtonClicked()
        {
            if (Progress.WorldData.ResourceData.TryBuy(ResourceType.Tokens, 100))
            {
                FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
                Progress.PlayerStats.UpgradeSpeed(10.0f);
                _saveLoadService.SaveProgress();
            }
        }

        private void OnUpgradeHPButtonClicked()
        {
            if (Progress.WorldData.ResourceData.TryBuy(ResourceType.Tokens, 100))
            {
                FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
                Progress.PlayerStats.UpgradeMaxHP(10.0f);
                _saveLoadService.SaveProgress();
            }
        }

        private void OnUpgradeTrunkButtonClicked()
        {
            if (Progress.WorldData.ResourceData.TryBuy(ResourceType.Tokens, 100))
            {
                FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
                Progress.PlayerStats.UpgradeMaxTrunk(5);
                _saveLoadService.SaveProgress();
            }
        }

        private void OnUpgradeGunButtonClicked()
        {
            if (Progress.WorldData.ResourceData.TryBuy(ResourceType.Tokens, 100))
            {
                FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
                Progress.PlayerStats.UpgradeGun(10.0f);
                _saveLoadService.SaveProgress();
            }
        }

        private void OnUpgradeDamageButtonClicked()
        {
            if (Progress.WorldData.ResourceData.TryBuy(ResourceType.Tokens, 100))
            {
                FindAnyObjectByType<AudioEffects>().AudioClickUpgr();
                Progress.PlayerStats.UpgradeDamage(10.0f);
                _saveLoadService.SaveProgress();
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerStats = Progress.PlayerStats;
            progress.WorldData.ResourceData = Progress.WorldData.ResourceData;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Progress.PlayerStats = progress.PlayerStats;
            Progress.WorldData.ResourceData = progress.WorldData.ResourceData;
            RefreshPlayerStatsText();
        }
    }
}