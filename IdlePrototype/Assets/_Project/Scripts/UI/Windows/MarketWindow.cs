using TMPro;

namespace IdleArcade.UI.Windows
{
    public class MarketWindow : UIWindowBase
    {
        //public TextMeshProUGUI ResourceText;

        protected override void Init() =>
            RefreshResourceTextText();

        protected override void SubscribeUpdates() => 
            Progress.WorldData.ResourceData.Changed += OnResourceChanged;

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.ResourceData.Changed -= OnResourceChanged;
        }

        private void OnResourceChanged(ResourceType type)
        {
            RefreshResourceTextText();
        }

        private string RefreshResourceTextText() => null;
            //ResourceText.text = Progress.WorldData.ResourceData.Collected.ToString();
    }
}