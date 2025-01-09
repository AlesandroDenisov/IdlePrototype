using System;
using TMPro;

namespace IdleArcade.UI.Windows
{
    public class WorkshopWindow : UIWindowBase
    {
        //public TextMeshProUGUI ResourceText;

        protected override void Init() =>
            RefreshResourcesTextText();

        protected override void SubscribeUpdates() => 
            Progress.WorldData.ResourceData.Changed += OnResourceChanged;

        private void OnResourceChanged(ResourceType type)
        {
            RefreshResourcesTextText();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.ResourceData.Changed -= OnResourceChanged;
        }

        private string RefreshResourcesTextText()
        {
            //ResourceText.text = Progress.WorldData.ResourceData.Collected.ToString();
            return null;
        }
    }
}