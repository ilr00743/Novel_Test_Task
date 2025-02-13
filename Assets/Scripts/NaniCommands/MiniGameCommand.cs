using Naninovel;
using Services.MiniGames;

namespace NaniCommands
{
    [CommandAlias("miniGame")]
    public class MiniGameCommand : Command
    {
        [ParameterAlias(NamelessParameterAlias), ResourceContext(MiniGamesConfiguration.DefaultPathPrefix)]
        public StringParameter Name;

        private MiniGamesService _miniGamesService;
        
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            if (!Assigned(Name)) return;

            _miniGamesService = Engine.GetService<MiniGamesService>();
            
            await _miniGamesService.InstantiateAsync(Name);
        }
    }
}