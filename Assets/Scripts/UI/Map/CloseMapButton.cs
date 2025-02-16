using Maps;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Map
{
    public class CloseMapButton : CustomUI
    {
        [SerializeField] private Button _closeButton;
        private IStateManager _stateManager;
        private IUIManager _uiManager;
        private IScriptPlayer _scriptPlayer;

        protected override void Start()
        {
            base.Start();
            _closeButton.onClick.AddListener(OnClose);
            _stateManager = Engine.GetService<IStateManager>();
            _uiManager = Engine.GetService<IUIManager>();
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
        }
        
        private void OnClose()
        {
            Hide();
            _stateManager.Configuration.EnableStateRollback = true;
            _uiManager.GetUI<OpenMapButton>().Show();
            _uiManager.GetUI<MapView>().Hide();
            _scriptPlayer.Play(_scriptPlayer.PlayedIndex);
        }
    }
}