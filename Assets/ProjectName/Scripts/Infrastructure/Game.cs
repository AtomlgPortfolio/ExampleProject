using ProjectName.Scripts.Infrastructure.Services;
using ProjectName.Scripts.Infrastructure.States;
using ProjectName.Scripts.Utilities;

namespace ProjectName.Scripts.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            ExceptionValidator.ThrowIfNull(coroutineRunner);
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
        }
    }
}