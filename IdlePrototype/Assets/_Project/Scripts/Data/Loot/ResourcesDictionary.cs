using System;
using System.Collections.Generic;
using System.Linq;

namespace IdleArcade.Data.Loot
{
    [Serializable]
    public class ResourcesDictionary
    {
        public ResourceType ResourceType;
        public int Amount;

        public ResourcesDictionary(ResourceType resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}
