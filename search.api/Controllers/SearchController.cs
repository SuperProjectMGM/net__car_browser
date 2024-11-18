using Microsoft.AspNetCore.Mvc;
using search.api.Interfaces;
using search.api.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace search.api.Controllers
{
    [Route("search.api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchInterface _service;

        public SearchController(ISearchInterface service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDefault()
        {
            // Pobieranie danych
            SearchInfo info = await _service.Search();
            var cars = info.Vehicles;

            // Konwertowanie pojazdów na DTO
            var dto = cars.Select(veh => veh.VehicleToVehicleOurDto()); // Zmienione na VehicleToVehicleOurDto()

            // Zwrócenie odpowiedzi
            return Ok(dto);
        }
    }
}
