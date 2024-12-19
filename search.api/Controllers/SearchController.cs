using Microsoft.AspNetCore.Mvc;
using search.api.DTOs;
using search.api.Interfaces;
using search.api.Mappers;
using SendGrid.Helpers.Mail;
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
        public async Task<ActionResult<VehicleOurDto>> GetAvailableCars(
    [FromQuery] DateTime rentalFrom,
    [FromQuery] DateTime rentalTo,
    [FromQuery] string location)
        {
            
            // Sprawdzamy, czy lokalizacja została podana
            if (string.IsNullOrWhiteSpace(location))
            {
                return BadRequest(new { message = "Lokalizacja jest wymagana" });
            }

            // Pobieranie danych pojazdów
            SearchInfo info = await _service.Search(rentalFrom, rentalTo);
            var cars = info.Vehicles;
            var availableCars = cars.Where(veh =>
                string.Equals(veh.Localization, location, StringComparison.OrdinalIgnoreCase))
                .Select(veh => veh.VehicleToVehicleOurDto());

            //Sprawdzamy, czy są dostępne samochody
            if (!availableCars.Any())
            {
                return NotFound(new { message = "Brak dostępnych samochodów w podanym okresie i lokalizacji" });
            }

            return Ok(availableCars);
        }



    }
}
