using IdleArcade.Data.Loot;
using System;
using UnityEngine.Tilemaps;

namespace IdleArcade.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public ResourceData ResourceData;

        public Stats PlayerStats;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            ResourceData = new ResourceData();

            PlayerStats = new Stats();
            KillData = new KillData();
        }
    }
}