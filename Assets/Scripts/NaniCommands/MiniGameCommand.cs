using System.Text;
using Naninovel;
using Naninovel.Commands;
using Services.MiniGames;

namespace NaniCommands
{
    [CommandAlias("miniGame")]
    public class MiniGameCommand : ShowUI
    {
        [RequiredParameter, ParameterAlias(NamelessParameterAlias), ResourceContext(MiniGamesConfiguration.DefaultPathPrefix)]
        public StringParameter Name;
        
        [ParameterAlias("goto"), EndpointContext]
        public NamedStringParameter Goto;
        
        private IMiniGamesService _miniGamesService;
        private IUIManager _uiManager;
        
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            // await base.ExecuteAsync(asyncToken);
            string gotoScript = null;
            
            if (Assigned(Goto))
            {
                var builder = new StringBuilder();
                builder.AppendLine($"@{nameof(Goto)} {Goto.Name ?? string.Empty}{(Goto.NamedValue.HasValue ? $".{Goto.NamedValue.Value}" : string.Empty)}");
                gotoScript = builder.ToString();
            }

            _miniGamesService = Engine.GetService<IMiniGamesService>();
            _uiManager = Engine.GetService<IUIManager>();

            var miniGame = await _miniGamesService.InstantiateAsync(Name, gotoScript);
            
            await miniGame.ChangeVisibilityAsync(true, Assigned(Duration) ? Duration : null);

        }
    }
}