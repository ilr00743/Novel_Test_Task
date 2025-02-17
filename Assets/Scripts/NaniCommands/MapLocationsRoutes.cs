using Naninovel;
using Services.Map;

namespace NaniCommands
{
    [CommandAlias("mapLocationsRoutes")]
    public class MapLocationsRoutes : Command
    {
        [RequiredParameter, ParameterAlias("locations")] 
        public StringListParameter Locations;

        [RequiredParameter, ParameterAlias("destinations"), EndpointContext]
        public StringListParameter GotoScripts;

        [ParameterAlias("disableLocations")]
        public StringListParameter DisableLocations;

        private MapService _mapService;
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            _mapService = Engine.GetService<MapService>();
            
            _mapService.SetDestinations(Locations, GotoScripts);

            if (Assigned(DisableLocations))
            {
                _mapService.SetLocationsActiveState(false, DisableLocations);
            }
            else
            {
                _mapService.SetLocationsActiveState(true);
            }
            
            return UniTask.CompletedTask;
        }
    }
}