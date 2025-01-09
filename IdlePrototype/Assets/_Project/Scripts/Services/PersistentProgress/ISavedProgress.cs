using IdleArcade.Data;

namespace IdleArcade.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}