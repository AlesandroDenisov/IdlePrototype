using IdleArcade.UI.Services.UIWindows;
using UnityEngine;
using UnityEngine.UI;

namespace IdleArcade.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public WindowId WindowId;
        private IUIWindowService _windowService;

        public void Init(IUIWindowService windowService) => 
            _windowService = windowService;

        private void Awake() => 
            Button.onClick.AddListener(Open);

        private void Open() => 
            _windowService.Open(WindowId);
    }
}