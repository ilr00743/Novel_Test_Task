using Naninovel;
using Naninovel.UI;
using UI;
using UI.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Maps
{
    [RequireComponent(typeof(PlayScript))]
    public class Location : ChoiceHandlerButton
    {
        [SerializeField] private string _name;
        
        private PlayScript _player;
        private Button _button;
        private IUIManager _uiManager;
        private string _scriptToPlay;

        public string Name => _name;
        
        protected override void Start()
        {
            base.Start();
            _player = GetComponent<PlayScript>();
            _button = GetComponent<Button>();
            _uiManager = Engine.GetService<IUIManager>();
            _button.onClick.AddListener(Play);
        }

        public void SetScriptToPlay(string scriptToPlay)
        {
            _scriptToPlay = scriptToPlay;
        }

        private void Play()
        {
            if (string.IsNullOrWhiteSpace(_scriptToPlay)) 
                return;
            
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            var stateManager = Engine.GetService<IStateManager>();
            
            _uiManager.GetUI<MapView>().Hide();
            _uiManager.GetUI<CloseMapButton>().Hide();
            stateManager.Configuration.EnableStateRollback = true;
            
            var text = $"@Goto {_scriptToPlay}";
            scriptPlayer.PlayTransient($"`{name}` generated script", text).Forget();
        }
    }
}