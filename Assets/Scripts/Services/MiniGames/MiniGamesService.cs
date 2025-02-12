using System.IO;
using Naninovel;
using UnityEngine;

namespace Services.MiniGames
{
    [InitializeAtRuntime]
    public class MiniGamesService : IEngineService<MiniGamesConfiguration>, IStatefulService<GameStateMap>
    {
        private readonly IResourceProviderManager _resourceProviderManager;
        private IResourceLoader<MiniGame> _resourceLoader;
        private MiniGame _currentGame;
            
        public IResourceLoader<MiniGame> Loader => _resourceLoader;

        public MiniGamesService(IResourceProviderManager resourceProviderManager)
        {
            _resourceProviderManager = resourceProviderManager;
        }
        
        public UniTask InitializeServiceAsync()
        {
            _resourceLoader = Configuration.Loader.CreateFor<MiniGame>(_resourceProviderManager);
            return UniTask.CompletedTask;
        }

        public async UniTask<MiniGame> LoadAsync(string name) 
            => await _resourceLoader.LoadAndHoldAsync(name, this);
        
        public void InstantiateMiniGame(MiniGame game)
        {
            if (_currentGame != null)
            {
                DestroyMiniGame();
            }

            _currentGame = GameObject.Instantiate(game);
        }

        private void DestroyMiniGame()
        {
            if (_currentGame != null)
            {
                GameObject.Destroy(_currentGame.gameObject);
                _resourceLoader.Release(Path.Combine(Configuration.PathPrefix, _currentGame.Name), this);
                _currentGame = null;
            }
        }

        public void ResetService()
        {
            DestroyMiniGame();
            _resourceLoader.ReleaseAll(this);
            Debug.Log("<color=red>[Mini Games Service]</color> Resources released");
        }

        public void DestroyService() { }

        public MiniGamesConfiguration Configuration 
            => ProjectConfigurationProvider.LoadOrDefault<MiniGamesConfiguration>();

        public void SaveServiceState(GameStateMap state) { }

        public UniTask LoadServiceStateAsync(GameStateMap state)
        {
            DestroyMiniGame();
            return UniTask.CompletedTask;
        }
    }
}