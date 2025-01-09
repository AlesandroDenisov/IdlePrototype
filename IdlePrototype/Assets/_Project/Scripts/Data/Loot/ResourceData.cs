using System;
using System.Collections.Generic;
using System.Linq;

namespace IdleArcade.Data.Loot
{
    [Serializable]
    public class ResourceData
    {
        public List<ResourcesDictionary> Resources = new List<ResourcesDictionary>();
        public LootComponentDataDictionary LootPiecesOnScene = new LootComponentDataDictionary();

        public Action<ResourceType> Changed;

        public ResourceData()
        {
            // Initialize resources with default values
            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                Resources.Add(new ResourcesDictionary(resourceType, 0));
            }
        }

        public void Collect(Loot loot)
        {
            var resource = Resources.FirstOrDefault(r => r.ResourceType == loot.ResourceType);
            if (resource != null)
            {
                resource.Amount += loot.Value;
                Changed?.Invoke(loot.ResourceType);
            }
        }

        public int GetResourceAmount(ResourceType resourceType)
        {
            var resource = Resources.FirstOrDefault(r => r.ResourceType == resourceType);
            return resource?.Amount ?? 0;
        }

        public bool TryBuy(ResourceType resourceType, int cost)
        {
            var resource = Resources.FirstOrDefault(r => r.ResourceType == resourceType);
            if (resource != null && resource.Amount >= cost)
            {
                resource.Amount -= cost;
                Changed?.Invoke(resourceType);
                return true;
            }
            return false;
        }

        public bool ExchangeResources(ResourceType fromResource, ResourceType toResource, int fromAmount, int toAmount)
        {
            var fromResourceData = Resources.FirstOrDefault(r => r.ResourceType == fromResource);
            var toResourceData = Resources.FirstOrDefault(r => r.ResourceType == toResource);

            if (fromResourceData != null && toResourceData != null && fromResourceData.Amount >= fromAmount)
            {
                fromResourceData.Amount -= fromAmount;
                toResourceData.Amount += toAmount;
                Changed?.Invoke(fromResource);
                Changed?.Invoke(toResource);
                return true;
            }
            return false;
        }
    }
}