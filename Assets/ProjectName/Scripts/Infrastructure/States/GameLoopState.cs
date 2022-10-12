using ProjectName.Scripts.Utilities;

namespace ProjectName.Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine)
        {
            ExceptionValidator.ThrowIfNull(stateMachine);
            _stateMachine = stateMachine;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}