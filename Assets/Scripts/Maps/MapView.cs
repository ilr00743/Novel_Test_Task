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
        private IMapService _mapService;

        protected override void Start()
        {
            base.Start();
            _mapService = Engine.GetService<IMapService>();
        }

        // Referenced in UnityEvent
        public void OnShow()
        {
            UpdateLocations();
        }

        private void UpdateLocations()
        {
            var locations = _mapService.MapState.Locations;

            if (locations.Count == 0) return;
            
            foreach (var data in locations)
            {
                _locations?.Find(location => location.Name == data.Location)?.SetScriptToPlay(data.Destination);
                _locations?.Find(location => location.Name == data.Location)?.SetStatus(data.IsActive);

            }
        }
    }
}