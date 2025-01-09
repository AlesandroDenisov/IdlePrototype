using IdleArcade.Data.Loot;
using System;
using UnityEngine.Serialization;

namespace IdleArcade.Data
{
    // When saving user data, do not save it in Unity types
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public ResourceData ResourceData;
        public WeaponData WeaponData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            ResourceData = new ResourceData();
            WeaponData = new WeaponData();
        }
    }
}