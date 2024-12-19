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
        public const string BasePath = "/api/Vehicle/available";

        public SearchMainService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        // TODO: tu musimy daty przekazywac i pobierac juz samochody dla danego zakresu
        public async Task<SearchInfo> Search(DateTime start, DateTime end)
        {
            var queryString = $"?start={start:yyyy-MM-dd}&end={end:yyyy-MM-dd}";
            var response = await _client.GetAsync($"{BasePath}{queryString}");
            var listOfVehiclesDto = await response.ReadContentAsync<List<VehicleOurDto>>();
            // I don't know now how to manage Id from logged or authenticated user
            SearchInfo info = new SearchInfo();
            info.Vehicles = listOfVehiclesDto.Select(dto => dto.VehicleOurDtoToVehicle()).ToList();
            return info;
        }
    }
}