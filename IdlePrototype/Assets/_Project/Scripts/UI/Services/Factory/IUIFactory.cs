using IdleArcade.Services;

namespace IdleArcade.UI.Services.Factory
{
    public interface IUIFactory: IService
    {
        void CreateUIRoot();
        void CreateGarage();
        void CreateMarket();
        void CreateWorkshop();

    }
}