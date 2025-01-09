using IdleArcade.Data;
using TMPro;
using UnityEngine;

namespace IdleArcade.UI
{
    public class ResourceCounter : MonoBehaviour
    {
        public ResourceType ResourceType;
        public TextMeshProUGUI Amount;
        private WorldData _worldData;

        public void Construct(WorldData worldData, ResourceType resourceType)
        {
            _worldData = worldData;
            ResourceType = resourceType;
            _worldData.ResourceData.Changed += UpdateAmount;
        }

        private void Start()
        {
            UpdateAmount(ResourceType);
        }

        private void UpdateAmount(ResourceType resourceType)
        {
            if (resourceType == ResourceType)
                Amount.text = $"{_worldData.ResourceData.GetResourceAmount(resourceType)}";
        }

        private void OnDestroy()
        {
            _worldData.ResourceData.Changed -= UpdateAmount;
        }
    }
}