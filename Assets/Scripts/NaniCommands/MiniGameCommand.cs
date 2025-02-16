using System.Text;
using Naninovel;
using Services.MiniGames;
using Command = Naninovel.Command;

namespace NaniCommands
{
    [CommandAlias("miniGame")]
    public class MiniGameCommand : Command
    {
        [RequiredParameter, ParameterAlias(NamelessParameterAlias), ResourceContext(MiniGamesConfiguration.DefaultPathPrefix)]
        public StringParameter Name;
        
        [ParameterAlias("goto"), EndpointContext]
        public NamedStringParameter Goto;
        
        private MiniGamesService _miniGamesService;
        
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            string gotoScript = null;
            
            if (Assigned(Goto))
            {
                var builder = new StringBuilder();
                builder.AppendLine($"@{nameof(Goto)} {Goto.Name ?? string.Empty}{(Goto.NamedValue.HasValue ? $".{Goto.NamedValue.Value}" : string.Empty)}");
                gotoScript = builder.ToString();
            }

            _miniGamesService = Engine.GetService<MiniGamesService>();

            await _miniGamesService.InstantiateAsync(Name, gotoScript);
        }
    }
}