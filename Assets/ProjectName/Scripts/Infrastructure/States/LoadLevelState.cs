using System.Threading.Tasks;
using ProjectName.Scripts.Infrastructure.Factory;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using ProjectName.Scripts.Infrastructure.Services.StaticData;
using ProjectName.Scripts.Logic;
using ProjectName.Scripts.Logic.CharacterLogic;
using ProjectName.Scripts.StaticData;
using ProjectName.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectName.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IPersistentProgressService progressService,
            IStaticDataService staticDataService)
        {
            ExceptionValidator.ThrowIfNull(stateMachine, sceneLoader, gameFactory, progressService, staticDataService);
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
        }

        public void Enter(string sceneName)
        {
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        public void Exit()
        {
        }

        private async void OnLoaded()
        {
            await InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private async Task InitGameWorld()
        {
            LevelStaticData levelData = GetLevelStaticData();
            GameObject characterGameObject = await InitCharacter(levelData);
            Character character = characterGameObject.GetComponent<Character>();
            ExceptionValidator.ThrowIfNull(character);
            character.Initialize();
            character.Activate();
            CameraFollow(characterGameObject);
        }

        private async Task<GameObject> InitCharacter(LevelStaticData levelData)
        {
            GameObject hero = await _gameFactory.CreateHero(levelData.InitialHeroPosition);
            ExceptionValidator.ThrowIfNull(hero);
            return hero;
        }

        private void CameraFollow(GameObject gameObject)
        {
            Camera camera = Camera.main;
            ExceptionValidator.ThrowIfNull(camera);

            if (camera.TryGetComponent(out CameraFollow cameraFollow))
                cameraFollow.Follow(gameObject);
            else
                ExceptionValidator.ThrowIfNull(cameraFollow);
        }

        private LevelStaticData GetLevelStaticData()
        {
            LevelStaticData levelStaticData = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
            ExceptionValidator.ThrowIfNull(levelStaticData);
            return levelStaticData;
        }
    }
}