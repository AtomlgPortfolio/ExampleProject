using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectName.Scripts.Infrastructure.AssetsManagement;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using ProjectName.Scripts.Utilities;
using UnityEngine;

namespace ProjectName.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assetsProvider;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject Hero { get; set; }

        public GameFactory(IAssets assetsProvider)
        {
            ExceptionValidator.ThrowIfNull(assetsProvider);
            _assetsProvider = assetsProvider;
        }

        public async Task<GameObject> CreateHero(Vector3 initialPointPosition)
        {
            Hero = await InstantiateRegisteredAsync(AssetsAddress.CharacterPath, at: initialPointPosition);
            return Hero;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assetsProvider.CleanUp();
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public void Unregister(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Remove(progressWriter);

            ProgressReaders.Remove(progressReader);
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
        {
            GameObject gameObject = await _assetsProvider.InstantiateAsync(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}