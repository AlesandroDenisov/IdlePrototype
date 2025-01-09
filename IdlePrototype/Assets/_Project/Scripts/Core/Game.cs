using IdleArcade.Services.Input;
using IdleArcade.Core.States;
using IdleArcade.Services;
using IdleArcade.Logic;

namespace IdleArcade.Core
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}
