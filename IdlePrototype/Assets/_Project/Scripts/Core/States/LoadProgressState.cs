using IdleArcade.Data;
using IdleArcade.Services.PersistentProgress;
using IdleArcade.Services.SaveLoad;

namespace IdleArcade.Core.States
{
    public class LoadProgressState : IState
    {
        private const string MainScene = "Main";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;

        public LoadProgressState( GameStateMachine gameStateMachine
                                , IPersistentProgressService progressService
                                , ISaveLoadService saveLoadProgress)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
      
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadProgress.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            return new PlayerProgress(initialLevel: MainScene);
        } 
    }
}