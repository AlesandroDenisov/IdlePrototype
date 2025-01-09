using IdleArcade.Services;

namespace IdleArcade.UI.Services.UIWindows
{
    public interface IUIWindowService : IService
    {
        void Open(WindowId windowId);
    }
}