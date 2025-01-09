using IdleArcade.StaticData;
using IdleArcade.StaticData.Windows;

namespace IdleArcade.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemies(EnemyTypeId typeId);
        WindowConfig ForWindow(WindowId shop);
    }
}