using IdleArcade.Core.AssetManagement;
using IdleArcade.Core.Factory;
using IdleArcade.Services;
using IdleArcade.Services.Input;
using IdleArcade.Services.PersistentProgress;
using IdleArcade.Services.SaveLoad;
using IdleArcade.Services.StaticData;
using IdleArcade.UI.Services.Factory;
using IdleArcade.UI.Services.UIWindows;
using UnityEngine;

namespace IdleArcade.Core.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "InitialScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        // Register all services.
        // Always start with the scene 'InitialScene'.
        public void Enter()
        {
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        private void RegisterServices()
        {
            RegisterStaticDataService();
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IUIFactory>(new UIFactory( _services.Single<IAssetProvider>()
                                                              , _services.Single<IStaticDataService>()
                                                              , _services.Single<IPersistentProgressService>()) );
            _services.RegisterSingle<IUIWindowService>(new UIWindowService(_services.Single<IUIFactory>()) );
            _services.RegisterSingle<IGameFactory>(new GameFactory( _services.Single<IAssetProvider>()
                                                                  , _services.Single<IStaticDataService>()
                                                                  , _services.Single<IPersistentProgressService>()
                                                                  , _services.Single<IUIWindowService>()
                                                                  ));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>()
                                                                          , _services.Single<IGameFactory>()) );
        }
        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadEnemies();
            _services.RegisterSingle(staticData);
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
            /*            #if UNITY_STANDALONE
                                    return new StandaloneInputService();
                        #elif UNITY_IOS || UNITY_ANDROID
                                    return new MobileInputService();
                        #endif*/
        }
    }
}