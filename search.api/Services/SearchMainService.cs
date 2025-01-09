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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        // Now it's hardcoded path. I think that we should develop something more interesting than hardcoded paths.
        public const string BasePath = "/api/Vehicle/available";
        public const string ExternalFirstPath = "";

        public SearchMainService(HttpClient client, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<SearchInfo> Search(DateTime start, DateTime end)
        {
            var queryString = $"?start={start:O}&end={end:O}";
            // Here we go to endpoint and have cars
            var baseUrl = _configuration["HttpClientSettingsBaseUrl"];
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl!);
            var response = await client.GetAsync($"{BasePath}{queryString}");
            var listOfVehiclesDto = await response.ReadContentAsync<List<VehicleOurDto>>();
            // After ours vehicles we should obtain url from another apis and get from them

    //---------------
            // var baseUrl = _configuration["HttpClientSettingsBaseUrl"];
            // var client = _httpClientFactory.CreateClient();
            // client.BaseAddress = new Uri(baseUrl!);
            // var response = await client.GetAsync($"{BasePath}{queryString}");
            // var listOfVehiclesDto = await response.ReadContentAsync<List<VehicleOurDto>>();
            var resList = new List<Vehicle>();
            // (Here is important to change data from dto of another api to ours)
            // Main idea is to add every list from every soure to one big list and to store it to frontend
    //---------------

            // Here our VehicleDto   
            // I don't know now how to manage Id from logged or authenticated user
            SearchInfo info = new SearchInfo();
            
            info.Vehicles = listOfVehiclesDto.Select(dto => dto.VehicleOurDtoToVehicle()).ToList();

            return info;
        }
    }
}