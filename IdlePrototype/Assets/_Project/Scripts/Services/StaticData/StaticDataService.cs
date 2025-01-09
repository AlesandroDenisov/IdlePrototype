using System.Collections.Generic;
using System.Linq;
using IdleArcade.StaticData;
using IdleArcade.StaticData.Windows;
using UnityEngine;

namespace IdleArcade.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EnemiesDataPath = "StaticData/Enemies";
        private const string StaticDataWindowPath = "StaticData/UI/WindowStaticData";

        private Dictionary<EnemyTypeId, EnemyStaticData> _enemyDict;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void LoadEnemies()
        {
            //_enemyDict = Resources.LoadAll<EnemyStaticData>(EnemiesDataPath).ToDictionary(x => x.EnemyTypeId, x => x);

            EnemyStaticData[] enemyDatas = Resources.LoadAll<EnemyStaticData>(EnemiesDataPath);

            _enemyDict = new Dictionary<EnemyTypeId, EnemyStaticData>();

            foreach (EnemyStaticData enemyData in enemyDatas)
            {
                if (_enemyDict.ContainsKey(enemyData.EnemyTypeId))
                {
                    Debug.LogWarning($"Duplicate enemy name found: {enemyData.EnemyTypeId}. Skipping duplicate.");
                    continue;
                }

                _enemyDict.Add(enemyData.EnemyTypeId, enemyData);
            }

            _windowConfigs = Resources.Load<WindowStaticData>(StaticDataWindowPath).Configs.ToDictionary(x => x.WindowId, x => x);
        }

        public EnemyStaticData ForEnemies(EnemyTypeId typeId) 
        {
            return _enemyDict.TryGetValue(typeId, out EnemyStaticData staticData) ? staticData : null;
        }

        public WindowConfig ForWindow(WindowId windowId)
        {
            return _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) ? windowConfig : null;
        }
    }
}