using System;

namespace IdleArcade.Data.Loot
{
    [Serializable]
    public class LootComponentData
    {
        public Vector3Data Position;
        public Loot Loot;

        public LootComponentData(Vector3Data position, Loot loot)
        {
            Position = position;
            Loot = loot;
        }
    }
}