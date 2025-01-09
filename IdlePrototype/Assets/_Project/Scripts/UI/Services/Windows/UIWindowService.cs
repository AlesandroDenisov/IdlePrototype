using IdleArcade.UI.Services.Factory;

namespace IdleArcade.UI.Services.UIWindows
{
    public class UIWindowService : IUIWindowService
    {
        private readonly IUIFactory _uiFactory;

        public UIWindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.None:
                    break;
                case WindowId.GarageWindow:
                    _uiFactory.CreateGarage();
                    break;
                case WindowId.MarketWindow:
                    _uiFactory.CreateMarket();
                    break;
                case WindowId.WorkshopWindow:
                    _uiFactory.CreateWorkshop();
                    break;
            }
        }
    }
}