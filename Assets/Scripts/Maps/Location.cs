﻿using Naninovel;
using Naninovel.UI;
using UI.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Maps
{
    public class Location : ChoiceHandlerButton
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _enabledIcon;
        [SerializeField] private Sprite _disabledIcon;
        [SerializeField] private Image _currentIcon;

        private Button _button;
        private IUIManager _uiManager;
        private string _scriptToPlay;
        private bool _isActive;

        public string Name => _name;
        
        protected override void Start()
        {
            base.Start();
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
            if (string.IsNullOrEmpty(_scriptToPlay)) 
                return;
            
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            var stateManager = Engine.GetService<IStateManager>();
            
            _uiManager.GetUI<MapView>().Hide();
            _uiManager.GetUI<CloseMapButton>().Hide();
            stateManager.Configuration.EnableStateRollback = true;
            
            var text = $"@Goto {_scriptToPlay}\r\n";
            scriptPlayer.PlayTransient($"`{name}` generated script", text).Forget();
        }

        public void SetStatus(bool isActive)
        {
            _isActive = isActive;
            
            _currentIcon.sprite = _isActive ? _enabledIcon : _disabledIcon;
        }
    }
}