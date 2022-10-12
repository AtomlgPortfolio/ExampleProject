using ProjectName.Scripts.Data;
using ProjectName.Scripts.Data.Extensions;
using ProjectName.Scripts.Infrastructure.Factory;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using ProjectName.Scripts.Utilities;
using UnityEngine;

namespace ProjectName.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            ExceptionValidator.ThrowIfNull(progressService, gameFactory);
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialize<PlayerProgress>();
    }
}