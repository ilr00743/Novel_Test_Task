using System.Collections.Generic;
using Naninovel;
using Naninovel.UI;
using Services;
using Services.Map;
using UnityEngine;

namespace Maps
{
    public class MapView : CustomUI
    {
        [SerializeField] private List<Location> _locations;
        private MapService _mapService;

        protected override void Start()
        {
            base.Start();
            _mapService = Engine.GetService<MapService>();
        }

        // Referenced in UnityEvent
        public void OnShow()
        {
            UpdateLocations();
        }

        private void UpdateLocations()
        {
            var locationDestinationPairs = _mapService.MapState.LocationDestinationPairs;

            if (locationDestinationPairs == null) return;
            
            foreach (var pair in locationDestinationPairs)
            {
                _locations?.Find(location => location.Name == pair.Key)?.SetScriptToPlay(pair.Value);
            }
        }
    }
}