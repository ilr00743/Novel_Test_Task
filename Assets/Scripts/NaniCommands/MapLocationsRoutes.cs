using Naninovel;
using Services;
using Services.Map;

namespace NaniCommands
{
    [CommandAlias("mapLocationsRoutes")]
    public class MapLocationsRoutes : Command
    {
        [RequiredParameter, ParameterAlias("locations")] 
        public StringListParameter Locations;

        [RequiredParameter, ParameterAlias("destinations")]
        public StringListParameter GotoScripts;

        private MapService _mapService;
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            _mapService = Engine.GetService<MapService>();
            
            _mapService.SetDestinations(Locations, GotoScripts);
            
            return UniTask.CompletedTask;
        }
    }
}