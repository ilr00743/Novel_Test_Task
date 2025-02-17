using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel;
using UnityEngine;

namespace Services.Map
{
    
    [Serializable]
    public class MapState
    {
        [SerializeField] public List<LocationData> Locations = new();
    }

    [InitializeAtRuntime]
    public class MapService : IStatefulService<GameStateMap>
    {
        public MapState MapState { get; private set; } = new();

        public UniTask InitializeServiceAsync()
        {
            Engine.GetService<StateManager>();
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

        public void SetDestinations(List<string> locations, List<string> scriptsToPlay)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                MapState.Locations.Add(new LocationData{Location = locations[i], Destination = scriptsToPlay[i]});
            }
        }

        public void SetLocationsActiveState(bool isActive, List<string> locationsNames = null)
        {
            if (locationsNames == null)
            {
                MapState.Locations.ForEach(location => location.IsActive = isActive);
                return;
            }
            
            var selectedLocations = new HashSet<string>(locationsNames);

            foreach (var location in MapState.Locations)
            {
                location.IsActive = selectedLocations.Contains(location.Location) ? isActive : true;
            }
        }
    }
}