using IdleArcade.Core.AssetManagement;
using IdleArcade.Services.PersistentProgress;
using IdleArcade.Services.StaticData;
using IdleArcade.StaticData.Windows;
using IdleArcade.UI.Windows;
using UnityEngine;

namespace IdleArcade.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory( IAssetProvider assets
                        , IStaticDataService staticData
                        , IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateUIRoot()
        {
            _uiRoot = _assets.Instantiate(AssetPath.UIRootPath).transform;
        }

        public void CreateGarage()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.GarageWindow);
            if (config != null)
            {
                UIWindowBase window = Object.Instantiate(config.Template, _uiRoot);
                window.Construct(_progressService);
                window.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Garage window config not found.");
            }
        }

        public void CreateMarket()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.MarketWindow);
            UIWindowBase window = Object.Instantiate(config.Template, _uiRoot);
            window.Construct(_progressService);
        }

        public void CreateWorkshop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.WorkshopWindow);
            UIWindowBase window = Object.Instantiate(config.Template, _uiRoot);
            window.Construct(_progressService);
        }
    }
}