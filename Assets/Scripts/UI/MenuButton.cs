using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuButton : CustomUI
    {
        [SerializeField] private Button _button;
        private IUIManager _uiManager;
        
        protected override void Start()
        {
            base.Start();
            _button.onClick.AddListener(OpenMenu);
            _uiManager = Engine.GetService<IUIManager>();
        }

        private void OpenMenu()
        {
            _uiManager.GetUI<IPauseUI>().Show();
        }
    }
}