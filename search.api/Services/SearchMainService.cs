using search.api.DTOs;
using search.api.Interfaces;
using search.api.Helpers;
using search.api.Mappers;

namespace search.api.Services
{
    public class SearchMainService : ISearchInterface
    {
        // Here We must ask api and after that create a SearchInfo object
        private readonly IConfiguration _configuration;
        private readonly IEnumerable<IVehicleGetter> _vehicleGetters;
        // Now it's hardcoded path. I think that we should develop something more interesting than hardcoded paths.
        public const string BasePath = "/api/Vehicle/available";
        public const string ExternalFirstPath = "";

        public SearchMainService(IConfiguration configuration, IEnumerable<IVehicleGetter> vehicleGetters)
        {
            _vehicleGetters = vehicleGetters;
            _configuration = configuration;
        }

        public async Task<SearchInfo> Search(DateTime start, DateTime end)
        {
            // var queryString = $"?start={start:O}&end={end:O}";
            // Here we go to endpoint and have cars
            var resList = new List<Vehicle>();
            foreach (var getter in _vehicleGetters)
            {
                var tmpList = await getter.GetAvailableVehiclesFromRemoteOrigin(start, end);
                resList.AddRange(tmpList.Select(dto => dto.VehicleOurDtoToVehicle()).ToList());
            }            
            SearchInfo info = new SearchInfo();
            
            info.Vehicles = resList;
            return info;
        }
    }
}