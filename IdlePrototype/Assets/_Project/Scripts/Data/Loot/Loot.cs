using System;

namespace IdleArcade.Data.Loot
{
    [Serializable]
    public class Loot
    {
        public int Value;
        public ResourceType ResourceType;

        public Loot(int value, ResourceType resourceType) 
        {
            Value = value;
            ResourceType = resourceType;
        }
    }
}