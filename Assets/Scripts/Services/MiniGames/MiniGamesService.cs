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
        private GameObject _currentGame;
        private MiniGameState _state = new();
        
        public MiniGamesConfiguration Configuration 
            => ProjectConfigurationProvider.LoadOrDefault<MiniGamesConfiguration>();

        public MiniGamesService(IResourceProviderManager resourceProviderManager)
        {
            _resourceProviderManager = resourceProviderManager;
        }

        public UniTask InitializeServiceAsync()
        {
            _resourceLoader = Configuration.Loader.CreateFor<GameObject>(_resourceProviderManager);
            return UniTask.CompletedTask;
        }

        public async UniTask InstantiateAsync(string name)
        {
            Reset();
            
            var resource = await LoadAsync(name);
            
            if (resource == null)
            {
                Debug.LogError($"<color=red>[Mini Games Service]</color> Can't load {name}");
                return;
            }
            
            Debug.Log($"<color=red>[Mini Games Service]</color> {name} is loaded");
            
            _state.Name = name;
            _state.IsActive = true;

            _currentGame = GameObject.Instantiate(resource);

            _currentGame.GetComponent<MiniGame>().Completed += Reset;
        }

        public async UniTask<GameObject> LoadAsync(string name) 
            => await _resourceLoader.LoadAsync(name);

        private void Reset()
        {
            if (_currentGame != null)
            {
                _currentGame.GetComponent<MiniGame>().Completed -= Reset;
                GameObject.Destroy(_currentGame);
                _currentGame = null;
            }
            
            _state.Name = null;
            _state.IsActive = false;
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
            state.SetState(_state);
        }

        public async UniTask LoadServiceStateAsync(GameStateMap state)
        {
            Reset();
            
            _state = state.GetState<MiniGameState>();
            
            if (_state.IsActive && !string.IsNullOrEmpty(_state.Name))
            {
                await InstantiateAsync(_state.Name);
            }
        }
    }
}