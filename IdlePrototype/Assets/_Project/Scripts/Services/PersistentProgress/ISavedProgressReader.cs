using IdleArcade.Data;

namespace IdleArcade.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}