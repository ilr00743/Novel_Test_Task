using System;
using System.Collections.Generic;
using Naninovel;

namespace Services.Map
{
    [Serializable]
    public class MapState
    {
        public IReadOnlyDictionary<string, string> LocationDestinationPairs;
    }
    
    [InitializeAtRuntime]
    public class MapService : IStatefulService<GameStateMap>
    {
        public MapState MapState { get; private set; }
        private Dictionary<string, string> _locationDestinationPairs;

        public UniTask InitializeServiceAsync()
        {
            Engine.GetService<StateManager>();
            MapState = new MapState();
            _locationDestinationPairs = new Dictionary<string, string>();
            return UniTask.CompletedTask;
        }

        
        public void ResetService() { }

        public void DestroyService() { }
        
        public void SaveServiceState(GameStateMap state)
        {
            state.SetState(MapState);
        }

        public UniTask LoadServiceStateAsync(GameStateMap state)
        {
            MapState = state.GetState<MapState>();
            return UniTask.CompletedTask;
        }

        public void SetDestinations(List<string> locations, List<string> destinations)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                _locationDestinationPairs.Add(locations[i], destinations[i]);
            }
            
            MapState.LocationDestinationPairs = _locationDestinationPairs;
        }
    }
}