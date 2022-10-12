using ProjectName.Scripts.Data;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using ProjectName.Scripts.Infrastructure.Services.SaveLoad;
using ProjectName.Scripts.Utilities;

namespace ProjectName.Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string InitialLevelName = "Main";

        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            ExceptionValidator.ThrowIfNull(stateMachine, progressService, saveLoadService);
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress(InitialLevelName)
            {
                HeroState =
                {
                    MaxHp = 50f
                },

                HeroStats =
                {
                    Damage = 1f,
                    DamageRadius = 0.5f,
                    MovementSpeed = 5f
                }
            };

            progress.HeroState.ResetHp();
            return progress;
        }
    }
}