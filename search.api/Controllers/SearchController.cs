using Microsoft.AspNetCore.Mvc;
using search.api.Interfaces;
using search.api.Mappers;
using System;
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
        public async Task<IActionResult> GetAvailableCars([FromQuery] string rentalFrom, [FromQuery] string rentalTo)
        {
            // Próbujemy sparsować daty z query stringa
            if (!DateTime.TryParse(rentalFrom, out DateTime parsedRentalFrom) ||
                !DateTime.TryParse(rentalTo, out DateTime parsedRentalTo))
            {
                return BadRequest(new { message = "Nieprawidłowy format daty" });
            }

            // Pobieranie danych pojazdów
            SearchInfo info = await _service.Search();
            var cars = info.Vehicles;

            // Filtracja dostępnych samochodów
            var availableCars = cars.Where(veh =>
                (parsedRentalFrom >= veh.RentalTo || parsedRentalTo <= veh.RentalFrom))
                .Select(veh => veh.VehicleToVehicleOurDto());

            // Sprawdzamy, czy są dostępne samochody
            if (!availableCars.Any())
            {
                return NotFound(new { message = "Brak dostępnych samochodów w podanym okresie" });
            }

            return Ok(availableCars);
        }

    }
}
