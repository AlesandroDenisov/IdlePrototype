using UnityEngine;
using IdleArcade.Logic;
using IdleArcade.Core.States;

namespace IdleArcade.Core
{
    public class GameEntryPointBootstrap : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        public LoadingCurtain CurtainPrefab;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
