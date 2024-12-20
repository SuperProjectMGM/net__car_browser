using search.api.DTOs;
using search.api.Interfaces;
using search.api.Helpers;
using search.api.Mappers;

namespace search.api.Services
{
    public class SearchMainService : ISearchInterface
    {
        // Here We must ask api and after that create a SearchInfo object
        private readonly HttpClient _client;
        // Now it's hardcoded path. I think that we should develop something more interesting than hardcoded paths.
        public const string BasePath = "/api/VehiclesDetail";

        public SearchMainService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<SearchInfo> Search()
        {
            var response = await _client.GetAsync(BasePath);
            var listOfVehiclesDto = await response.ReadContentAsync<List<VehicleOurDto>>();
            // I don't know now how to manage Id from logged or authenticated user
            SearchInfo info = new SearchInfo();
            info.Vehicles = listOfVehiclesDto.Select(dto => dto.VehicleOurDtoToVehicle()).ToList();
            return info;
        }
    }
}