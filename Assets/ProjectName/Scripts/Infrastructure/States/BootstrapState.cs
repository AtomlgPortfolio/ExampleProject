using ProjectName.Scripts.Infrastructure.AssetsManagement;
using ProjectName.Scripts.Infrastructure.Factory;
using ProjectName.Scripts.Infrastructure.Services;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using ProjectName.Scripts.Infrastructure.Services.SaveLoad;
using ProjectName.Scripts.Infrastructure.Services.StaticData;
using ProjectName.Scripts.Services.Inputs;
using ProjectName.Scripts.Utilities;
using UnityEngine;

namespace ProjectName.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            ExceptionValidator.ThrowIfNull(stateMachine, sceneLoader, services);
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }


        public void Enter() =>
            _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticDataService();
            RegisterAssetProvider();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>()
            ));

            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IPersistentProgressService>(),
                    _services.Single<IGameFactory>()));
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadData();
            _services.RegisterSingle(staticDataService);
        }

        private void RegisterAssetProvider()
        {
            AssetProvider assetProvider = new AssetProvider();
            _services.RegisterSingle<IAssets>(assetProvider);
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();

            return new MobileInputService();
        }
    }
}