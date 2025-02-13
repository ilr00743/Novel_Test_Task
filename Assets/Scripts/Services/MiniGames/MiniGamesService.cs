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

        public async UniTask<GameObject> LoadAsync(string name) 
            => await _resourceLoader.LoadAndHoldAsync(name,this);

        public void InstantiateMiniGame(GameObject game)
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
                var name = _currentGame.GetComponent<MiniGame>().Name;
                _resourceLoader.Release(name, this);
                GameObject.Destroy(_currentGame);
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

        public void SaveServiceState(GameStateMap state) { }

        public UniTask LoadServiceStateAsync(GameStateMap state)
        {
            DestroyMiniGame();
            return UniTask.CompletedTask;
        }
    }
}