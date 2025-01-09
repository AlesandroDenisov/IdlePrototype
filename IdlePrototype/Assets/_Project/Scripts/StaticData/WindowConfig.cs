using System;
using IdleArcade.UI.Windows;

namespace IdleArcade.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public UIWindowBase Template;
    }
}