using Naninovel;

namespace Services.MiniGames
{
    public interface IMiniGamesService : IStatefulService<GameStateMap>
    {
        MiniGameState MiniGameState { get; }
        UniTask<MiniGame> InstantiateAsync(string gameName, string onContinueScript = null);
    }
}