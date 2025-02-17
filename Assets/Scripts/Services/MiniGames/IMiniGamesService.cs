using Naninovel;
using UnityEngine;

namespace Services.MiniGames
{
    public interface IMiniGamesService : IStatefulService<GameStateMap>
    {
        UniTask InstantiateAsync(string gameName, string onContinueScript = null);
    }
}