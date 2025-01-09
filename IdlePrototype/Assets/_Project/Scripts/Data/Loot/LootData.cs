using System.Collections.Generic;
using System;

namespace IdleArcade.Data
{
    [Serializable]
    public class LootData
    {
        public List<string> LootedSpawners = new List<string>();
    }
}