using System.IO;
using Naninovel;
using UnityEngine;

namespace Services.MiniGames
{
    [InitializeAtRuntime]
    public class MiniGamesService : IEngineService<MiniGamesConfiguration>, IStatefulService<GameStateMap>
    {
        private readonly IResourceProviderManager _resourceProviderManager;
        private IResourceLoader<GameObject> _resourceLoader;
        private IScriptPlayer _scriptPlayer;
        private IStateManager _stateManager;
        
        private GameObject _currentGame;
        public MiniGameState MiniGameState { get; private set; } = new();
        
        public MiniGamesConfiguration Configuration 
            => ProjectConfigurationProvider.LoadOrDefault<MiniGamesConfiguration>();

        public MiniGamesService(IResourceProviderManager resourceProviderManager)
        {
            _resourceProviderManager = resourceProviderManager;
        }

        public UniTask InitializeServiceAsync()
        {
            _resourceLoader = Configuration.Loader.CreateFor<GameObject>(_resourceProviderManager);
            _scriptPlayer = Engine.GetService<IScriptPlayer>();
            _stateManager = Engine.GetService<IStateManager>();
            
            return UniTask.CompletedTask;
        }

        public async UniTask InstantiateAsync(string gameName, string onContinueScript = null)
        {
            Reset();
            
            MiniGameState.Name = gameName;
            MiniGameState.IsActive = true;
            MiniGameState.OnContinueScript = onContinueScript;
            
            var resource = await LoadAsync(gameName);

            if (resource == null)
            {
                Debug.LogWarning($"<color=red>[Mini Games Service]</color> Can't load {gameName}. Will be loaded first mini game from config.");
                //TODO: Loading FirstOrDefault from config
                MiniGameState.Name = gameName; //name of firstOrDefault loaded game
                return;
            }
            
            Debug.Log($"<color=red>[Mini Games Service]</color> {gameName} is loaded");
            
            _currentGame = GameObject.Instantiate(resource);

            _currentGame.GetComponent<MiniGame>().Completed += ContinuePlay;
        }

        public async UniTask<GameObject> LoadAsync(string name) 
            => await _resourceLoader.LoadAsync(name);

        private void ContinuePlay()
        {
            if (!string.IsNullOrEmpty(MiniGameState.OnContinueScript))
            {
                _scriptPlayer.PlayTransient(_scriptPlayer.PlayedScript.Name, MiniGameState.OnContinueScript);
            }
            else
            {
                _scriptPlayer.Play(_scriptPlayer.PlayedIndex + 1);
            }
            
            Reset();
        }
        
        private void Reset()
        {
            if (_currentGame != null)
            {
                _currentGame.GetComponent<MiniGame>().Completed -= ContinuePlay;
                GameObject.Destroy(_currentGame);
                _currentGame = null;
            }
            
            MiniGameState.Name = null;
            MiniGameState.IsActive = false;
            MiniGameState.OnContinueScript = null;
        }

        public void ResetService()
        {
            Reset();
            Debug.Log("<color=red>[Mini Games Service]</color> Resources released");
        }

        public void DestroyService()
        {
            _resourceLoader.ReleaseAll(this);
        }

        public void SaveServiceState(GameStateMap state)
        {
            state.SetState(MiniGameState);
        }

        public async UniTask LoadServiceStateAsync(GameStateMap state)
        {
            Reset();
            
            MiniGameState = state.GetState<MiniGameState>();
            
            if (!_stateManager.RollbackInProgress && MiniGameState.IsActive && !string.IsNullOrEmpty(MiniGameState.Name))
            {
                await InstantiateAsync(MiniGameState.Name);
            }
        }
    }
}