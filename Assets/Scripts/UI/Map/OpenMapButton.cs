using Maps;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Map
{
    public class OpenMapButton : CustomUI
    {
        [SerializeField] private Button _openButton;
        private IStateManager _stateManager;
        private IUIManager _uiManager;
        private IScriptPlayer _scriptPlayer;

        protected override void Start()
        {
            base.Start();
            _openButton.onClick.AddListener(OnOpen);
            _stateManager = Engine.GetService<IStateManager>();
            _uiManager = Engine.GetService<IUIManager>();
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
        }
        
        private void OnOpen()
        {
            Hide();
            _stateManager.Configuration.EnableStateRollback = false;
            _uiManager.GetUI<CloseMapButton>().Show();
            _uiManager.GetUI<MapView>().Show();
            _scriptPlayer.Stop();
        }
    }
}