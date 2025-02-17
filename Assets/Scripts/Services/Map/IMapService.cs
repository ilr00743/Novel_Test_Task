using System.Collections.Generic;
using Naninovel;

namespace Services.Map
{
    public interface IMapService : IStatefulService<GameStateMap>
    {
        MapState MapState { get; }
        void SetDestinations(List<string> locations, List<string> scriptsToPlay);
        void SetLocationsActiveState(bool isActive, List<string> locationsNames = null);
        
    }
}